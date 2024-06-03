using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Response;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/profile
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/profile/{id}
        * Description: Người dùng thực hiện lấy dữ liệu người dùng trên hệ thống
        */
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] long id)
        {
            try
            {
                var data = await _profileService.GetProfileById(id);

                // Check if data not exist
                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Người dùng không tồn tại!");
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu người dùng thành công!",
                    Data = data,
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu người dùng!");
                }
            }
        }
    }
}
