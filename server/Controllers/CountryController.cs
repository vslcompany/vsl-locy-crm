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
    * URL: https://localhost:portnumber/api/v1/country
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CountryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/country/all
        * Description: Lấy dữ liệu về tất cả quốc gia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var data = await _categoryService.GetAllCountries();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả quốc gia thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả quốc gia!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/country
        * Description: Lấy dữ liệu về quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetCountries(Start, Size, search);
                var total = await _categoryService.GetTotalCountries(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu quốc gia thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu quốc gia!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/country
        * Description: Tạo mới dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequest req)
        {
            try
            {
                await _categoryService.CreateCountry(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu quốc gia thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu quốc gia trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/country/{id}
        * Description: Cập nhật dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCountry([FromRoute] long id, [FromBody] UpdateCountryRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu quốc gia trên hệ thống!");
                }

                var data = await _categoryService.GetCountryById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu quốc gia trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateCountry(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu quốc gia thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu quốc gia trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/country/{id}
        * Description: Xoá dữ liệu quốc gia trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetCountryById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu quốc gia trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeleteCountry(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu quốc gia thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu quốc gia trên hệ thống!");
                }
            }
        }
    }
}
