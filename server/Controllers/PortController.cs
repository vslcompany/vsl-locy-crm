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
    * URL: https://localhost:portnumber/api/v1/port
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class PortController : Controller
    {
        private readonly ICategoryService _categoryService;

        public PortController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/port/{idCountry}/all
        * Description: Lấy dữ liệu về cảng ứng với quốc gia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{idCountry}/all")]
        public async Task<IActionResult> GetPortsByIdCountry([FromRoute] long idCountry)
        {
            try
            {
                var data = await _categoryService.GetPortsByIdCountry(idCountry);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về cảng ứng với quốc gia thành công!",
                    Data = data,
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về cảng ứng với quốc gia!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/port
        * Description: Lấy dữ liệu về cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetPorts([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetPorts(Start, Size, search);
                var total = await _categoryService.GetTotalPorts(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu cảng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu cảng!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/port
        * Description: Tạo mới dữ liệu cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreatePort([FromBody] CreatePortRequest req)
        {
            try
            {
                await _categoryService.CreatePort(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu cảng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu cảng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/port/{id}
        * Description: Cập nhật dữ liệu cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePort([FromRoute] long id, [FromBody] UpdatePortRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu cảng trên hệ thống!");
                }

                var data = await _categoryService.GetPortById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu cảng trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdatePort(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu cảng thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu cảng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/port/{id}
        * Description: Xoá dữ liệu cảng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePort([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetPortById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu cảng trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeletePort(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu cảng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu cảng trên hệ thống!");
                }
            }
        }
    }
}
