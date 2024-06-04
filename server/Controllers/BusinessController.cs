using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Responses;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Exceptions;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/business
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class BusinessController : Controller
    {
        private readonly ICategoryService _categoryService;

        public BusinessController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/business/all
        * Description: Lấy dữ liệu về tất cả loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBusinesses()
        {
            try
            {
                var data = await _categoryService.GetAllBusinesses();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả loại doanh nghiệp thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả loại doanh nghiệp!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/business
        * Description: Lấy dữ liệu về loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetBusinesses([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetBusinesses(Start, Size, search);
                var total = await _categoryService.GetTotalBusinesses(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu loại doanh nghiệp thành công!",
                    Data = new
                    {
                        data = data,
                        totalRow = total,
                    }
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu loại doanh nghiệp!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/business
        * Description: Tạo mới dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest req)
        {
            try
            {
                await _categoryService.CreateBusiness(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu loại doanh nghiệp thành công!",
                    Data = null,
                };

                return StatusCode(201, response);
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu loại doanh nghiệp trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/business/{id}
        * Description: Cập nhật dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBusiness([FromRoute] long id, [FromBody] UpdateBusinessRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống!");
                }

                var data = await _categoryService.GetBusinessById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateBusiness(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu loại doanh nghiệp thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu loại doanh nghiệp trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/business/{id}
        * Description: Xoá dữ liệu loại doanh nghiệp trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBusiness([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetBusinessById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu loại doanh nghiệp trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeleteBusiness(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu loại doanh nghiệp thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu loại doanh nghiệp trên hệ thống!");
                }
            }
        }
    }
}
