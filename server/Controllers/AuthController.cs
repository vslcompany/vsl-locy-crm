using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/auth
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService<TblSysUser> _authService;
        private readonly IProfileService _profileService;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly TimeSpan _cachedTime = TimeSpan.FromMinutes(5);
        private readonly ICacheService _cacheService;

        public AuthController(IAuthService<TblSysUser> authService, IProfileService profileService, IConfiguration config, IEmailService emailService, ICacheService cacheService)
        {
            _authService = authService;
            _profileService = profileService;
            _config = config;
            _emailService = emailService;
            _cacheService = cacheService;
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/login
        * Description: Người dùng thực hiện đăng nhập trên hệ thống
        */
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequest req)
        {
            // Check value request
            if (req.Username.ToLower().Trim() == "" || req.Password.ToLower().Trim() == "")
            {
                throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Bạn cần nhập thông tin đăng nhập đầy đủ!");
            }

            try
            {
                var hashPassword = _authService.hashPassword(req.Password);
                var data = await _authService.getByUsernameAndPassword(req.Username, hashPassword);

                // Check if data not exist
                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Tên đăng nhập hoặc mật khẩu sai. Vui lòng nhập đúng thông tin!");
                }

                if (data.Active == false)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Bạn không thể đăng nhập vào hệ thống. Vui lòng liên hệ admin để được giải quyết!");
                }

                // Get data for response to client
                var payload = await _profileService.GetProfileById(data.Id);

                // Check user is deleted?
                if (payload != null && payload.flagDelete == true)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Bạn không thể đăng nhập vào hệ thống. Vui lòng liên hệ admin để được giải quyết!");
                }


                // Create token and refresh token
                var isSecure = HttpContext.Request.IsHttps;
                var token = _authService.createToken(data, 43200 /* 30 days */);
                var refreshToken = _authService.createToken(data, 129600 /* 90 days */);
                var cookieOptionRefreshToken = _authService.getCookieOptions(90, isSecure);

                // Save refresh token to cookie
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptionRefreshToken);

                var response = new Response()
                {
                    Status = true,
                    Message = "Chào mừng " + payload?.hoTenVI + " đã đăng nhập thành công vào hệ thống!",
                    Data = new
                    {
                        token,
                        payload,
                    },
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi đăng nhập. Vui lòng thử lại sau!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/logout
        * Description: Người dùng thực hiện đăng xuất trên hệ thống
        */
        [HttpPost]
        [Route("logout")]
        public IActionResult LogOut()
        {
            try
            {
                // Remove refresh token in cookie
                var isSecure = HttpContext.Request.IsHttps;
                var cookieOptionRefreshToken = _authService.getCookieOptions(90, isSecure);
                Response.Cookies.Delete("refreshToken", cookieOptionRefreshToken);

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã đăng xuất thành công!",
                    Data = null,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi đăng xuất. Vui lòng thử lại sau!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/refresh-token
        * Description: Tái đăng ký thời hạn đăng nhập
        */
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest req)
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var data = await _authService.getById(req.Id);

                if (refreshToken == null || data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi thực hiện tái ký thời hạn đăng nhập!");
                }

                // Checking refresh token is expired or not
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtKey = _config["Jwt:Key"] ?? "";
                var jwtIssuer = _config["Jwt:Issuer"] ?? "";
                var jwtAudience = _config["Jwt:Audience"] ?? "";
                var key = Encoding.UTF8.GetBytes(jwtKey);

                try
                {
                    tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, // This checks if the token is expired
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var expClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;

                    if (expClaim != null && long.TryParse(expClaim, out var exp))
                    {
                        var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                        if (expDate < DateTime.UtcNow)
                        {
                            throw new ErrorException((int)HttpStatusCode.Unauthorized, "Unauthorized", "Lỗi thực hiện tái ký thời hạn đăng nhập!");
                        }
                    }
                }
                catch
                {
                    throw new ErrorException((int)HttpStatusCode.Unauthorized, "Unauthorized", "Lỗi thực hiện tái ký thời hạn đăng nhập!");
                }

                // Create token and refresh token
                var isSecure = HttpContext.Request.IsHttps;
                var newToken = _authService.createToken(data, 43200 /* 30 days */);
                var newRefreshToken = _authService.createToken(data, 129600 /* 90 days */);
                var cookieOptionNewRefreshToken = _authService.getCookieOptions(90, isSecure);

                // Store prev refresh token in caching


                // Save refresh token to cookie
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptionNewRefreshToken);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tái ký thời hạn đăng nhập thành công!",
                    Data = new
                    {
                        token = newToken,
                    },
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Có lỗi nào đó khi thực hiện tái ký thời hạn đăng nhập!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/change-password
        * Description: Người dùng thực hiện đổi mật khẩu
        */
        [Authorize]
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest req)
        {
            try
            {
                var data = await _authService.getById(req.Id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Đổi mật khẩu thất bại vì mật khẩu cũ không chính xác!");
                }

                var hashPassword = _authService.hashPassword(req.Password.ToLower().Trim());
                var hashNewPassword = _authService.hashPassword(req.NewPassword.ToLower().Trim());

                if (data.Password != hashPassword)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Đổi mật khẩu thất bại vì mật khẩu cũ không chính xác!");
                }

                if (hashPassword != hashNewPassword)
                {
                    await _authService.changePassword(data, hashNewPassword);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Đổi mật khẩu thành công thành công!",
                    Data = null,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Đổi mật khẩu thất bại. Vui lòng thử lại!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/forgot-password
        * Description: Người dùng thực hiện đổi mật khẩu
        */
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest req)
        {
            try
            {
                var data = await _authService.getByEmail(req.Email);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Không tồn tại email trong hệ thống!");
                }

                // Create OTP
                var otp = "";
                var keyCache = $"otp-{req.Email}";
                // Get caching otp value
                var cachedOtp = _cacheService.GetValue(keyCache);
                // If not exist
                if(string.IsNullOrEmpty(cachedOtp))
                {
                    // Random otp number code
                    Random generate = new Random();
                    var randNum = generate.Next(1000000);
                    otp = randNum.ToString("D6");

                    // Save otp to caching
                    _cacheService.SetValue(keyCache, otp, _cachedTime);

                    // Sending email with new OTP
                    await _emailService.SendEmailAsync(req.Email, "Mã OTP dành cho việc lấy lại mật khẩu trên LOCY CRM", $"Mã OTP của bạn là: {otp}");
                } else
                {
                    otp = cachedOtp;
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Hệ thống đã gửi mã OTP vào gmail của bạn. Vui lòng kiểm tra gmail của bạn!",
                    Data = null,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi thực hiện tính năng quên mật khẩu!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/check-otp
        * Description: Người dùng thực hiện đổi mật khẩu
        */
        [HttpPost]
        [Route("check-otp")]
        public async Task<IActionResult> CheckOTP([FromBody] CheckOTPRequest req)
        {
            try
            {
                var keyCache = $"otp-{req.Email}";
                // Get caching otp value
                var cachedOtp = _cacheService.GetValue(keyCache);

                if (string.IsNullOrEmpty(cachedOtp))
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Mã OTP không hợp lệ!");
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn có thể đặt lại mật khẩu!",
                    Data = null,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi kiểm tra OTP reset mật khẩu!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/auth/check-otp
        * Description: Người dùng thực hiện đổi mật khẩu
        */
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest req)
        {
            try
            {
                var keyCache = $"otp-{req.Email}";
                // Get caching otp value
                var cachedOtp = _cacheService.GetValue(keyCache);

                if (!string.IsNullOrEmpty(cachedOtp)) {
                    // Remove otp in cache
                    _cacheService.Remove(keyCache);
                } else
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi reset mật khẩu!");
                }

                // hash password
                var data = await _authService.getByEmail(req.Email);

                if (data != null)
                {
                    var hashPassword = _authService.hashPassword(req.Password.ToLower().Trim());
                    await _authService.changePassword(data, hashPassword);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã đặt lại mật khẩu thành công!",
                    Data = null,
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi reset mật khẩu!");
                }
            }
        }
    }
}
