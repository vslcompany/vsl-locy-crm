using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/employee
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employee
        * Description: Người dùng thực hiện lấy danh sách nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQuery query)
        {
            try
            {
                var data = await _employeeService.GetData(query);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả nhân viên thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả nhân viên!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employee/group
        * Description: Người dùng thực hiện lấy danh sách nhân viên trong nhóm
        */
        [Authorize("DeliveryCustomer")]
        [HttpGet]
        [Route("group")]
        public async Task<IActionResult> GetEmployeesGroup()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idNhanVien = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";
                List<EmployeeDto>? data;

                if (permission.Contains("1048576") || permission.Contains("7000"))
                {
                    data = await _employeeService.GetAllEmployees();
                }
                else
                {
                    var ids = await _employeeService.GetListEmployee(idNhanVien);
                    data = (ids != null && ids.Count > 0) ? await _employeeService.GetAllEmployeesGroup(ids) : [];
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về tất cả nhân viên đang quản lý!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về tất cả nhân viên đang quản lý!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/employee
        * Description: Người dùng thực hiện tạo mới nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest req)
        {
            try
            {
                var isPersonnelCodeExist = await _employeeService.IsPersonnelCodeExist(req.manhanvien);

                if (isPersonnelCodeExist)
                {
                    throw new ErrorException((int)HttpStatusCode.Conflict, "Conflict data", "Mã nhân sự đã tồn tại, vui lòng nhập lại mã nhân sự!");
                }

                if (req.Username != null && req.Username != "")
                {
                    var isUsernameExist = await _employeeService.IsUsernameExist(req.Username);
                    if (isUsernameExist)
                    {
                        throw new ErrorException((int)HttpStatusCode.Conflict, "Conflict data", "Tên đăng nhập đã tồn tại, vui lòng nhập lại tên đăng nhập!");
                    }
                }

                await _employeeService.Create(req);

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã tạo nhân viên " + req.HoTenVI + " thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo nhân viên trên hệ thống. Vui lòng thử lại!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/employee/{id}
        * Description: Người dùng thực hiện cập nhật thông tin nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployeeById([FromRoute] long id, [FromBody] UpdateEmployeeRequest req)
        {
            try
            {
                if (id != req.IdNhanVien)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi cập nhật nhân viên trên hệ thống!");
                }

                // Get data
                var info = await _employeeService.GetEmployeeById(req.IdNhanVien);
                var account = await _employeeService.GetAccountById(req.Id);

                // Check if data is not exist
                if (info == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi cập nhật nhân viên trên hệ thống vì nhân viên không tồn tại!");
                }

                // Check personnel code is changed and personnel code is exist
                if (req.manhanvien != null && req.manhanvien != "" && req.manhanvien != info?.Manhansu)
                {
                    var isPersonnelCodeExist = await _employeeService.IsPersonnelCodeExist(req.manhanvien);

                    if (isPersonnelCodeExist)
                    {
                        throw new ErrorException((int)HttpStatusCode.Conflict, "Conflict data", "Mã nhân sự đã tồn tại, vui lòng nhập lại mã nhân sự!");
                    }
                }

                // Check username is changed and username is exist
                if (req.Username != null && req.Username != "" && req.Username != account?.UserName)
                {
                    var isUsernameExist = await _employeeService.IsUsernameExist(req.Username);

                    if (isUsernameExist)
                    {
                        throw new ErrorException((int)HttpStatusCode.Conflict, "Conflict data", "Tên đăng nhập đã tồn tại, vui lòng nhập lại tên đăng nhập!");
                    }
                }

                if (info != null)
                {
                    await _employeeService.Update(account, info, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Bạn đã cập nhật thông tin nhân viên " + info?.HoTenVI + " thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi cập nhật nhân viên trên hệ thống. Vui lòng thử lại!");
                }
            }
        }

        /**
        * Method -> Url: [PUT] -> https://localhost:portnumber/api/v1/employee/{id}/delete
        * Description: Người dùng thực hiện yêu cầu xoá / huỷ xoá nhân viên trên hệ thống
        */
        [Authorize("ManageEmployee")]
        [HttpPut]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id, [FromBody] DeleteEmployeeRequest req)
        {
            try
            {
                if (id != req.IdNhanVien)
                {
                    throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", !req.FlagDelete ? "Lỗi huỷ xoá nhân viên trên hệ thống!" : "Lỗi yêu cầu xoá nhân viên trên hệ thống!");
                }

                var data = await _employeeService.GetEmployeeById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", !req.FlagDelete ? "Lỗi huỷ xoá nhân viên vì nhân viên không tồn tại trên hệ thống!" : "Lỗi yêu cầu xoá nhân viên vì nhân viên không tồn tại trên hệ thống!");
                }

                if (data != null)
                {
                    await _employeeService.Delete(data, req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = !req.FlagDelete ? "Bạn đã huỷ xoá nhân viên " + data?.HoTenVI + " thành công!" : "Bạn đã yêu cầu xoá nhân viên " + data?.HoTenVI + " thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", !req.FlagDelete ? "Lỗi huỷ xoá nhân viên trên hệ thống!" : "Lỗi yêu cầu xoá nhân viên trên hệ thống!");
                }
            }
        }
    }
}
