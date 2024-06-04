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
    * URL: https://localhost:portnumber/api/v1/major
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class MajorController : Controller
    {
        private readonly ICategoryService _categoryService;

        public MajorController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/major/all
        * Description: Lấy dữ liệu về tất cả nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllMajors()
        {
            try
            {
                var data = await _categoryService.GetAllMajors();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả nghiệp vụ thành công!",
                    Data = data
                };

                return Ok(response);
            } catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả nghiệp vụ!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/major
        * Description: Lấy dữ liệu về nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetMajors([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetMajors(Start, Size, search);
                var total = await _categoryService.GetTotalMajors(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu nghiệp vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu nghiệp vụ!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/major
        * Description: Tạo mới dữ liệu nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateMajor([FromBody] CreateMajorRequest req)
        {
            try
            {
                await _categoryService.CreateMajor(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu nghiệp vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu nghiệp vụ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/major/{id}
        * Description: Cập nhật dữ liệu nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMajor([FromRoute] long id, [FromBody] UpdateMajorRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống!");
                }

                var data = await _categoryService.GetMajorById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateMajor(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu nghiệp vụ thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/major/{id}
        * Description: Xoá dữ liệu nghiệp vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMajor([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetMajorById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeleteMajor(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu nghiệp vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu nghiệp vụ trên hệ thống!");
                }
            }
        }
    }
}
