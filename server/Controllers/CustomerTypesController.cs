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
   * URL: https://localhost:portnumber/api/v1/customertypes
   */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CustomerTypesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CustomerTypesController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customertypes/all
        * Description: Lấy dữ liệu về tất cả loại đánh giá khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCustomerTypes()
        {
            try
            {
                var data = await _categoryService.GetAllCustomerTypes();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả loại đánh giá khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả loại đánh giá khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customertypes?start=0&size=100&search=
        * Description: Lấy dữ liệu về loại đánh giá khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerTypes([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetCustomerTypes(Start, Size, search);
                var total = await _categoryService.GetTotalCustomerTypes(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về loại đánh giá khách hàng thành công!",
                    Data = new
                    {
                        data = data,
                        totalRow = total,
                    }
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về loại đánh giá khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customertypes
        * Description: Thêm mới dữ liệu về loại đánh giá khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomerType([FromBody] CreateCustomerTypeRequest req)
        {
            try
            {
                await _categoryService.CreateCustomerType(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo loại đánh giá khách hàng thành công trên hệ thống!",
                    Data = null
                };

                return StatusCode(201, response);
            } catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo loại đánh giá khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customertypes/{id}
        * Description: Cập nhật dữ liệu về loại đánh giá khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomerType([FromRoute] long id, [FromBody] UpdateCustomerTypeRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật loại đánh giá khách hàng trên hệ thống!");
                }

                var data = await _categoryService.GetCustomerTypeById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật loại đánh giá khách hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateCustomerType(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật loại đánh giá khách hàng thành công trên hệ thống!",
                    Data = null,
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật loại đánh giá khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customertypes/{id}
        * Description: Xoá dữ liệu về loại đánh giá khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomerType([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetCustomerTypeById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá loại đánh giá khách hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _categoryService.DeleteCustomerType(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá loại đánh giá khách hàng thành công!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá loại đánh giá khách hàng trên hệ thống!");
                }
            }
        }
    }
}
