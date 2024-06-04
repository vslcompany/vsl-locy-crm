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
    * URL: https://localhost:portnumber/api/v1/city
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CityController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city/all
        * Description: Lấy dữ liệu về tất cả thành phố trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                var data = await _categoryService.GetAllCities();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả thành phố thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả thành phố!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city/{idQuocGia}/country
        * Description: Lấy dữ liệu về tất cả thành phố dựa theo trường idQuocgia trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{idQuocGia}/country")]
        public async Task<IActionResult> GetCitiesByIdCountry([FromRoute] long idQuocGia)
        {
            try
            {
                var data = await _categoryService.GetCitiesByIdCountry(idQuocGia);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả thành phố theo quốc gia thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả thành phố theo quốc gia!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/city
        * Description: Lấy dữ liệu về thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCities([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetCities(Start, Size, search);
                var total = await _categoryService.GetTotalCities(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu thành phố thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu thành phố!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/city
        * Description: Tạo mới dữ liệu thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityRequest req)
        {
            try
            {
                await _categoryService.CreateCity(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu thành phố thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu thành phố trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/city/{id}
        * Description: Cập nhật dữ liệu thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCity([FromRoute] long id, [FromBody] UpdateCityRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu thành phố trên hệ thống!");
                }

                var data = await _categoryService.GetCityById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu thành phố trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateCity(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu thành phố thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu thành phố trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/city/{id}
        * Description: Xoá dữ liệu thành phố trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetCityById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu thành phố trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeleteCity(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu thành phố thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu thành phố trên hệ thống!");
                }
            }
        }
    }
}
