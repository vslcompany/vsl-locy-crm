using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;

namespace vsl_crm_api.Services
{
    public class AuthService: IAuthService<TblSysUser>
    {
        // Config auth variables
        private readonly VslDbContext _db;
        private readonly IConfiguration _config;
        private readonly CookieOptions _cookieOptions;
        private const string _KeyECBPKCS7 = "VSL@213$171$198&&17192125";

        public AuthService(VslDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            _cookieOptions = new CookieOptions()
            {
                Domain = _config["Domain"],
                HttpOnly = false,
                Secure = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                IsEssential = true,
            };
        }

        public async Task changePassword(TblSysUser data, string newPassword)
        {
            data.Password = newPassword;
            await _db.SaveChangesAsync();
        }

        public string createToken(TblSysUser data, double expire)
        {
            if (data == null) return "";

            var jwtKey = _config["Jwt:Key"] ?? "";
            var jwtIssuer = _config["Jwt:Issuer"] ?? "";
            var jwtAudience = _config["Jwt:Audience"] ?? "";

            // Config jwt
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claim in jwt
            var claims = new[]
            {
                new Claim("Id", data.Id.ToString()),
            };

            // Token option
            var tokenOptions = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: credentials
            );

            // Create token
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public async Task<TblSysUser?> getByEmail(string email)
        {
            TblSysUser? data = await _db.TblSysUsers
                                        .Where(x => x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.Email == email)
                                        .FirstOrDefaultAsync();
            return data;
        }

        public async Task<TblSysUser?> getById(long id)
        {
            TblSysUser? data = await _db.TblSysUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<TblSysUser?> getByUsernameAndPassword(string username, string password)
        {
            TblSysUser? data = await _db.TblSysUsers.Where(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
            return data;
        }

        public CookieOptions getCookieOptions(double day, bool isSecure)
        {
            var options = _cookieOptions;
            options.Secure = isSecure;
            options.Expires = DateTimeOffset.UtcNow.AddDays(day);
            return options;
        }

        public string hashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_KeyECBPKCS7));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var transform = tdes.CreateEncryptor();
            byte[] textBytes = Encoding.UTF8.GetBytes(password);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);

            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
