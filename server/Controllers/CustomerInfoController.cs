using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/customerinfo
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class CustomerInfoController : Controller
    {
        private readonly ICustomerInfoService _customerInfoService;
        private readonly ICustomerService _customerService;

        public CustomerInfoController(ICustomerInfoService customerInfoService, ICustomerService customerService)
        {
            _customerInfoService = customerInfoService;
            _customerService = customerService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/{id}
        * Description: Lấy dữ liệu về một khách hàng theo id trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomersById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Dữ liệu khách hàng không tồn tại!");
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu khách hàng " + data.NameVI + " thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/imex/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("imex/{idCustomer}")]
        public async Task<IActionResult> GetCustomerListImEx([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerListImEx(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerListImEx(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách xuất nhập khẩu thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách xuất nhập khẩu!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/imex
        * Description: Thêm mới dữ liệu về dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("imex")]
        public async Task<IActionResult> CreateImEx([FromBody] CreateCustomerImExRequest req)
        {

            try
            {
                await _customerInfoService.CreateCustomerImEx(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu xuất nhập khẩu thành công trên hệ thống!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu xuất nhập khẩu trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/imex/{id}
        * Description: Cập nhật về dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("imex/{id}")]
        public async Task<IActionResult> UpdateImEx([FromBody] UpdateCustomerImExRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerImExById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerImEx(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu xuất nhập khẩu thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu xuất nhập khẩu trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/imex/{id}
        * Description: Xoá dữ liệu xuất nhập khẩu của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("imex/{id}")]
        public async Task<IActionResult> DeleteImEx([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerImExById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu xuất nhập khẩu trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerImEx(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu xuất nhập khẩu thành công!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu xuất nhập khẩu trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/operational/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("operational/{idCustomer}")]
        public async Task<IActionResult> GetCustomerOperationals([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerOperationals(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerOperationals(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách tác nghiệp thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách tác nghiệp!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/operational
        * Description: Thêm mới dữ liệu về dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("operational")]
        public async Task<IActionResult> CreateOperational([FromBody] CreateCustomerOperationalRequest req)
        {
            try
            {
                var customer = await _customerService.GetById(req.IdCustomer);
                await _customerInfoService.CreateCustomerOperational(req, customer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu tác nghiệp thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu tác nghiệp trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/operational/{id}
        * Description: Cập nhật về dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("operational/{id}")]
        public async Task<IActionResult> UpdateOperational([FromBody] UpdateCustomerOperationalRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerOperationalById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerOperational(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu tác nghiệp thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu tác nghiệp trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/operational/{id}
        * Description: Xoá dữ liệu tác nghiệp của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("operational/{id}")]
        public async Task<IActionResult> DeleteOperational([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerOperationalById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi xoá dữ liệu tác nghiệp trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerOperational(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu tác nghiệp thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu tác nghiệp trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/contact/{idCustomer}/all
        * Description: Lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("contact/{idCustomer}/all")]
        public async Task<IActionResult> GetAllCustomerContacts([FromRoute] long idCustomer = 0)
        {
            try
            {
                var data = await _customerInfoService.GetAllCustomerContacts(idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng thành công!",
                    Data = data
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách tất cả người liên hệ của một khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/contact/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("contact/{idCustomer}")]
        public async Task<IActionResult> GetCustomerContacts([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerContacts(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerContacts(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách người liên hệ thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách người liên hệ!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/contact
        * Description: Thêm mới dữ liệu về dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("contact")]
        public async Task<IActionResult> CreateContact([FromBody] CreateCustomerContactRequest req)
        {
            try
            {
                await _customerInfoService.CreateCustomerContact(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu người liên hệ thành công trên hệ thống!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu người liên hệ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/contact/{id}
        * Description: Cập nhật về dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("contact/{id}")]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateCustomerContactRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerContactById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerContact(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu người liên hệ thành công trên hệ thống!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu người liên hệ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/contact/{id}
        * Description: Xoá dữ liệu người liên hệ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("contact/{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerContactById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu người liên hệ trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerContact(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu người liên hệ thành công!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu người liên hệ trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("evaluate/{idCustomer}")]
        public async Task<IActionResult> GetCustomerEvaluates([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerEvaluates(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerEvaluates(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách đánh giá thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách đánh giá!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/evaluate
        * Description: Thêm mới dữ liệu về dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("evaluate")]
        public async Task<IActionResult> CreateEvaluate([FromBody] CreateCustomerEvaluateRequest req)
        {
            try
            {
                await _customerInfoService.CreateCustomerEvaluate(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu đánh giá thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu đánh giá trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{id}
        * Description: Cập nhật về dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("evaluate/{id}")]
        public async Task<IActionResult> UpdateEvaluate([FromBody] UpdateCustomerEvaluateRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu đánh giá trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerEvaluateById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu đánh giá trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerEvaluate(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu đánh giá thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu đánh giá trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/evaluate/{id}
        * Description: Xoá dữ liệu đánh giá của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("evaluate/{id}")]
        public async Task<IActionResult> DeleteEvaluate([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerEvaluateById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu đánh giá trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerEvaluate(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu đánh giá thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu đánh giá trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/classify/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("classify/{idCustomer}")]
        public async Task<IActionResult> GetCustomerClassifies([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerClassifies(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerClassifies(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách phân loại khách hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách phân loại khách hàng!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/classify
        * Description: Thêm mới dữ liệu về dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("classify")]
        public async Task<IActionResult> CreateClassify([FromBody] CreateCustomerClassifyRequest req)
        {
            try
            {
                await _customerInfoService.CreateCustomerClassify(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu phân loại khách hàng thành công trên hệ thống!",
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
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/classify/{id}
        * Description: Cập nhật về dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("classify/{id}")]
        public async Task<IActionResult> UpdateClassify([FromBody] UpdateCustomerClassifyRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerClassifyById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerClassify(data, req);
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
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/classify/{id}
        * Description: Xoá dữ liệu phân loại khách hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("classify/{id}")]
        public async Task<IActionResult> DeleteClassify([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerClassifyById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu phân loại khách hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerClassify(data);

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

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/major/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("major/{idCustomer}")]
        public async Task<IActionResult> GetCustomerMajors([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerMajors(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerMajors(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách nghiệp vụ thành công!",
                    Data = new
                    {
                        data = data,
                        totalRow = total
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách nghiệp vụ!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/major
        * Description: Thêm mới dữ liệu về dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("major")]
        public async Task<IActionResult> CreateMajor([FromBody] CreateCustomerMajorRequest req)
        {
            try
            {
                await _customerInfoService.CreateCustomerMajor(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu nghiệp vụ thành công trên hệ thống!",
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
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/major/{id}
        * Description: Cập nhật về dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("major/{id}")]
        public async Task<IActionResult> UpdateMajor([FromBody] UpdateCustomerMajorRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerMajorById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerMajor(data, req);
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
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/major/{id}
        * Description: Xoá dữ liệu nghiệp vụ của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("major/{id}")]
        public async Task<IActionResult> DeleteMajor([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerMajorById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu nghiệp vụ trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerMajor(data);

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

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/customerinfo/route/{idCustomer}?start=0&size=100&search=
        * Description: Lấy dữ liệu về danh sách tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("route/{idCustomer}")]
        public async Task<IActionResult> GetCustomerRoutes([FromQuery(Name = "start")] int Start = 0, [FromQuery(Name = "size")] int Size = 10, [FromQuery(Name = "search")] string Search = "", [FromRoute] long idCustomer = 0)
        {
            try
            {
                var search = Search.ToLower().Trim();
                var data = await _customerInfoService.GetCustomerRoutes(Start, Size, search, idCustomer);
                var total = await _customerInfoService.GetTotalCustomerRoutes(search, idCustomer);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách tuyến hàng thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách tuyến hàng!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/customerinfo/route
        * Description: Thêm mới dữ liệu về dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        [Route("route")]
        public async Task<IActionResult> CreateRoute([FromBody] CreateCustomerRouteRequest req)
        {
            try
            {
                await _customerInfoService.CreateCustomerRoute(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo dữ liệu tuyến hàng thành công trên hệ thống!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo dữ liệu tuyến hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/customerinfo/route/{id}
        * Description: Cập nhật về dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPut]
        [Route("route/{id}")]
        public async Task<IActionResult> UpdateRoute([FromBody] UpdateCustomerRouteRequest req, [FromRoute] long id = 0)
        {
            try
            {
                if (id != req.Id)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống!");
                }

                var data = await _customerInfoService.GetCustomerRouteById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                if (data != null)
                {
                    await _customerInfoService.UpdateCustomerRoute(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật dữ liệu tuyến hàng thành công trên hệ thống!",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật dữ liệu tuyến hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [DELETE] -> https://localhost:portnumber/api/v1/customerinfo/route/{id}
        * Description: Xoá dữ liệu tuyến hàng của một khách hàng trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("route/{id}")]
        public async Task<IActionResult> DeleteRoute([FromRoute] long id)
        {
            try
            {
                var data = await _customerInfoService.GetCustomerRouteById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi xoá dữ liệu tuyến hàng trên hệ thống vì dữ liệu không tồn tại!");
                }

                await _customerInfoService.DeleteCustomerRoute(data);

                var response = new Response()
                {
                    Status = true,
                    Message = "Xoá dữ liệu tuyến hàng thành công",
                    Data = null
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi xoá dữ liệu tuyến hàng trên hệ thống!");
                }
            }
        }
    }
}
