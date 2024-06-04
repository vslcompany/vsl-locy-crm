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
    * URL: https://localhost:portnumber/api/v1/typeofcustomer
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class TypeOfCustomerController : Controller
    {
        private readonly ICategoryService _categoryService;

        public TypeOfCustomerController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/typeofcustomer/all
        * Description: Lấy dữ liệu về tất cả phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTypeOfCustomers()
        {
            try
            {
                var data = await _categoryService.GetAllTypeOfCustomers();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả phân loại khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả phân loại khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/typeofcustomer
        * Description: Lấy dữ liệu về phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpGet]
        public async Task<IActionResult> GetTypeOfCustomers([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "")
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _categoryService.GetTypeOfCustomers(Start, Size, search);
                var total = await _categoryService.GetTotalTypeOfCustomers(search);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu phân loại khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu phân loại khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/typeofcustomer
        * Description: Tạo mới dữ liệu phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateTypeOfCustomer([FromBody] CreateTypeOfCustomerRequest req)
        {
            try
            {
                await _categoryService.CreateTypeOfCustomer(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu phân loại khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu phân loại khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/typeofcustomer/{id}
        * Description: Cập nhật dữ liệu phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTypeOfCustomer([FromRoute] long id, [FromBody] UpdateTypeOfCustomerRequest req)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống!");
                }

                var data = await _categoryService.GetTypeOfCustomerById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _categoryService.UpdateTypeOfCustomer(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu phân loại khách hàng thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/typeofcustomer/{id}
        * Description: Xoá dữ liệu phân loại khách hàng trong cơ sở dữ liệu
        */
        [Authorize("ManageCategory")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTypeOfCustomer([FromRoute] long id)
        {
            try
            {
                var data = await _categoryService.GetTypeOfCustomerById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại trên hệ thống!");
                }

                await _categoryService.DeleteTypeOfCustomer(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu phân loại khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu phân loại khách hàng trên hệ thống!");
                }
            }
        }
    }
}
