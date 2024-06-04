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
    * URL: https://localhost:portnumber/api/v1/position
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly ICategoryService _categoryService;

        public PositionController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/position/all
        * Description: Lấy dữ liệu về tất cả chức vụ trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPositions()
        {
            try
            {
                var data = await _categoryService.GetAllPositions();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả chức vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả chức vụ!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/position
        * Description: Lấy dữ liệu về chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetPositions([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetPositions(Start, Size, search);
                var total = await _categoryService.GetTotalPositions(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu chức vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu chức vụ!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/position
        * Description: Tạo mới dữ liệu chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest req)
        {
            try
            {
                await _categoryService.CreatePosition(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu chức vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu chức vụ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/position/{id}
        * Description: Cập nhật dữ liệu chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePosition([FromRoute] long id, [FromBody] UpdatePositionRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu chức vụ trên hệ thống!");
                }

                var data = await _categoryService.GetPositionById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu chức vụ trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdatePosition(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu chức vụ thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu chức vụ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/position/{id}
        * Description: Xoá dữ liệu chức vụ trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetPositionById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu chức vụ trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeletePosition(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu chức vụ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu chức vụ trên hệ thống!");
                }
            }
        }
    }
}
