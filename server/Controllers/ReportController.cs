using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/report
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IEmployeeService _employeeService;

        public ReportController(IReportService reportService, IEmployeeService employeeService)
        {
            _reportService = reportService;
            _employeeService = employeeService;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/report/work
        * Description: Lấy dữ liệu về báo cáo công việc thực hiện
        */
        [Authorize]
        [HttpGet]
        [Route("work")]
        public async Task<IActionResult> GetReportWorkList([FromQuery] ReportWorkQuery query)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUser = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "Id")?.Value ?? "0");
                var idEmployee = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";

                List<long> idEmployees = new List<long>();
                if (!permission.Contains("1048576") && !permission.Contains("7000") && permission.Contains("7080"))
                {
                    idEmployees = await _employeeService.GetListEmployee(idEmployee);
                }

                var data = await _reportService.GetReportWork(query, permission, idUser, idEmployees);

                var response = new Response()
                {
                    Status = true,
                    Message = "Lấy dữ liệu báo cáo công việc thực hiện thành công!",
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
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi lấy dữ liệu báo cáo công việc thực hiện!");
                }
            }
        }
    }
}
