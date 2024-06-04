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
    * URL: https://localhost:portnumber/api/v1/employeegroup
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class EmployeeGroupController : Controller
    {
        private readonly IEmployeeGroupService _employeeGroupService;

        public EmployeeGroupController(IEmployeeGroupService employeeGroupService)
        {
            _employeeGroupService = employeeGroupService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employeegroup/nogroup
        * Description: Lấy dữ liệu về danh sách nhân sự chưa có nhóm trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        [Route("nogroup")]
        public async Task<IActionResult> GetAllEmployeesNoGroup()
        {
            try
            {
                var data = await _employeeGroupService.GetEmployeeNoGroup();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách nhân sự chưa có nhóm thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách nhân sự chưa có nhóm!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/employeegroup
        * Description: Lấy dữ liệu về danh sách nhóm nhân sự trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeGroups()
        {
            try
            {
                var data = await _employeeGroupService.GetEmployeeGroups();
                var total = await _employeeGroupService.GetTotalEmployeeGroups();

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu về danh sách nhóm nhân sự thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu về danh sách nhóm nhân sự!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/employeegroup
        * Description: Tạo nhóm nhân sự trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeGroup([FromBody] CreateEmployeeGroupRequest req)
        {
            try
            {
                if (req.IdNhanVien.Length > 0)
                {
                    await _employeeGroupService.CreateGroup(req);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Tạo nhóm nhân sự thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi tạo nhóm nhân sự trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [POST] -> https://localhost:portnumber/api/v1/employeegroup/${id}
        * Description: Tạo nhóm nhân sự trong cơ sở dữ liệu
        */
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployeeGroupById([FromRoute] long id)
        {
            try
            {
                var data = await _employeeGroupService.GetEmployeeGroupById(id);

                if (data == null)
                {
                    throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "Lỗi loại nhân viên khỏi nhóm trên hệ thống!");
                }

                if (data != null)
                {
                    await _employeeGroupService.DeleteGroup(data);
                }

                var response = new Response()
                {
                    Status = true,
                    Message = "Loại nhân viên khỏi nhóm thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi loại nhân viên khỏi nhóm trên hệ thống!");
                }
            }
        }
    }
}
