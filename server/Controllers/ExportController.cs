using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;
using System.Net;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Responses;
using vsl_crm_api.Exceptions;
using vsl_crm_api.Models.DTOs;

namespace vsl_crm_api.Controllers
{
    /*
    * URL: https://localhost:portnumber/api/v1/export
    */
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ExportController : Controller
    {
        //private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExportController(/*ICustomerService customerService,*/ IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            // _customerService = customerService;
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/export/customer
        * Description: Người dùng thực hiện export dữ liệu khách hàng trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("customer")]
        public async Task<IActionResult> ExportData()
        {
            try
            {
                var pageSize = 1000;
                var pageNumber = 0;
                string baseFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates", "export-customer.xlsx");
                var filePath = Path.Combine(Path.GetTempPath(), "customer_data.xlsx");

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUser = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "Id")?.Value ?? "0");
                var idEmployee = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";

                using (var excelPackage = new ExcelPackage(new FileInfo(baseFilePath)))
                {
                    // Get the existing worksheet or create a new one if it doesn't exist
                    var worksheet = excelPackage.Workbook.Worksheets["Khach Hang"];

                    if (worksheet == null)
                    {
                        // Create a worksheet in the package
                        worksheet = excelPackage.Workbook.Worksheets.Add("Khach Hang");

                        // Write headers
                        worksheet.Cells[1, 1].Value = "MST";
                        worksheet.Cells[1, 2].Value = "Mã/Tên ngắn";
                        worksheet.Cells[1, 3].Value = "Tên VI";
                        worksheet.Cells[1, 4].Value = "Tên EN";
                        worksheet.Cells[1, 5].Value = "Địa chỉ VI";
                        worksheet.Cells[1, 6].Value = "Địa chỉ EN";
                        worksheet.Cells[1, 7].Value = "Thành phố";
                        worksheet.Cells[1, 8].Value = "Quốc Gia";
                        worksheet.Cells[1, 9].Value = "Điện thoại";
                        worksheet.Cells[1, 10].Value = "Số FAX";
                        worksheet.Cells[1, 11].Value = "Email";
                        worksheet.Cells[1, 12].Value = "Ghi chú";
                    }

                    int rowIndex = 2; // Start writing data from row 2

                    while (true)
                    {
                        List<long> idEmployees = await _employeeService.GetListEmployee(idEmployee);
                        //var data = await _customerService.GetCustomersData(pageNumber, pageSize, permission, idUser, idEmployee, idEmployees);
                        var data = new List<CustomerDto>();
                        if (data != null && !data.Any())
                        {
                            break;
                        }

                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                // Write data to the worksheet
                                worksheet.Cells[rowIndex, 1].Value = item.TaxCode ?? "";
                                worksheet.Cells[rowIndex, 2].Value = item.Code ?? "";
                                worksheet.Cells[rowIndex, 3].Value = item.NameVI ?? "";
                                worksheet.Cells[rowIndex, 4].Value = item.NameEN ?? "";
                                worksheet.Cells[rowIndex, 5].Value = item.AddressVI ?? "";
                                worksheet.Cells[rowIndex, 6].Value = item.AddressEN ?? "";
                                // worksheet.Cells[rowIndex, 7].Value = item.IdcityNavigation != null ? (item.IdcityNavigation.NameVI ?? "") : "";
                                // worksheet.Cells[rowIndex, 8].Value = item.IdquocGiaNavigation != null ? (item.IdquocGiaNavigation.NameVI ?? "") : "";
                                worksheet.Cells[rowIndex, 9].Value = item.Phone ?? "";
                                worksheet.Cells[rowIndex, 10].Value = item.Fax ?? "";
                                worksheet.Cells[rowIndex, 11].Value = item.Email ?? "";
                                worksheet.Cells[rowIndex, 12].Value = item.Note ?? "";

                                rowIndex++;
                            }
                        }

                        pageNumber++;
                    }

                    // Save the Excel file
                    excelPackage.SaveAs(new FileInfo(filePath));

                    var response = new Response()
                    {
                        Status = true,
                        Message = "Bạn đã export dữ liệu khách hàng thành công!",
                        Data = new
                        {
                            downloadUrl = Url.Action("DownloadFile", new { fileName = Path.GetFileName(filePath) })
                        }
                    };

                    return Ok(response);
                }
            } catch(Exception e) {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi export dữ liệu khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/export/customer/received
        * Description: Người dùng thực hiện export dữ liệu khách hàng trên hệ thống
        */
        [Authorize]
        [HttpGet]
        [Route("customer/received")]
        public async Task<IActionResult> ExportCustomersReceivedData()
        {
            try
            {
                var pageSize = 1000;
                var pageNumber = 0;
                string baseFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates", "export-customer.xlsx");
                var filePath = Path.Combine(Path.GetTempPath(), "customer_receive_data.xlsx");

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUser = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "Id")?.Value ?? "0");
                var idEmployee = long.Parse(identity?.Claims.FirstOrDefault(o => o.Type == "IDEmployee")?.Value ?? "0");
                var permission = identity?.Claims.FirstOrDefault(o => o.Type == "Permission")?.Value ?? "";

                using (var excelPackage = new ExcelPackage(new FileInfo(baseFilePath)))
                {
                    // Get the existing worksheet or create a new one if it doesn't exist
                    var worksheet = excelPackage.Workbook.Worksheets["Khach Hang"];

                    if (worksheet == null)
                    {
                        // Create a worksheet in the package
                        worksheet = excelPackage.Workbook.Worksheets.Add("Khach Hang");

                        // Write headers
                        worksheet.Cells[1, 1].Value = "MST";
                        worksheet.Cells[1, 2].Value = "Mã/Tên ngắn";
                        worksheet.Cells[1, 3].Value = "Tên VI";
                        worksheet.Cells[1, 4].Value = "Tên EN";
                        worksheet.Cells[1, 5].Value = "Địa chỉ VI";
                        worksheet.Cells[1, 6].Value = "Địa chỉ EN";
                        worksheet.Cells[1, 7].Value = "Thành phố";
                        worksheet.Cells[1, 8].Value = "Quốc Gia";
                        worksheet.Cells[1, 9].Value = "Điện thoại";
                        worksheet.Cells[1, 10].Value = "Số FAX";
                        worksheet.Cells[1, 11].Value = "Email";
                        worksheet.Cells[1, 12].Value = "Ghi chú";
                    }

                    int rowIndex = 2; // Start writing data from row 2

                    while (true)
                    {
                        List<long> idEmployees = await _employeeService.GetListEmployee(idEmployee);
                        // var data = await _customerService.GetCustomersReceivedData(pageNumber, pageSize, permission, idUser, idEmployee, idEmployees);
                        var data = new List<CustomerDto>();
                        if (data != null && !data.Any())
                        {
                            break;
                        }

                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                // Write data to the worksheet
                                worksheet.Cells[rowIndex, 1].Value = item.TaxCode ?? "";
                                worksheet.Cells[rowIndex, 2].Value = item.Code ?? "";
                                worksheet.Cells[rowIndex, 3].Value = item.NameVI ?? "";
                                worksheet.Cells[rowIndex, 4].Value = item.NameEN ?? "";
                                worksheet.Cells[rowIndex, 5].Value = item.AddressVI ?? "";
                                worksheet.Cells[rowIndex, 6].Value = item.AddressEN ?? "";
                                // worksheet.Cells[rowIndex, 7].Value = item.IdcityNavigation != null ? (item.IdcityNavigation.NameVI ?? "") : "";
                                // worksheet.Cells[rowIndex, 8].Value = item.IdquocGiaNavigation != null ? (item.IdquocGiaNavigation.NameVI ?? "") : "";
                                worksheet.Cells[rowIndex, 9].Value = item.Phone ?? "";
                                worksheet.Cells[rowIndex, 10].Value = item.Fax ?? "";
                                worksheet.Cells[rowIndex, 11].Value = item.Email ?? "";
                                worksheet.Cells[rowIndex, 12].Value = item.Note ?? "";

                                rowIndex++;
                            }
                        }

                        pageNumber++;
                    }

                    // Save the Excel file
                    excelPackage.SaveAs(new FileInfo(filePath));

                    var response = new Response()
                    {
                        Status = true,
                        Message = "Bạn đã export dữ liệu khách hàng thành công!",
                        Data = new
                        {
                            downloadUrl = Url.Action("DownloadFile", new { fileName = Path.GetFileName(filePath) })
                        }
                    };

                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                if (e is ErrorException errorException)
                {
                    throw errorException;
                }
                else
                {
                    throw new ErrorException((int)HttpStatusCode.InternalServerError, "Internal server error", "Lỗi export dữ liệu khách hàng trên hệ thống!");
                }
            }
        }

        /**
        * Method -> Url: [GET] -> https://localhost:portnumber/api/v1/export/download/{fileName}
        * Description: Người dùng thực hiện tải file export dữ liệu trên hệ thống
        */
        [HttpGet]
        [Route("download/{fileName}")]
        public IActionResult DownloadFile([FromRoute] string fileName)
        {
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    // Open the file stream without using the 'using' statement
                    var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    // Return the file to the client
                    var fileResult = File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    // Delete the file after serving the response
                    Response.OnCompleted(() =>
                    {
                        stream.Dispose(); // Dispose of the stream
                        System.IO.File.Delete(filePath);
                        return Task.CompletedTask;
                    });

                    return fileResult;
                } catch(Exception e) {
                    if (e is ErrorException errorException)
                    {
                        throw errorException;
                    }
                    else
                    {
                        throw new ErrorException((int)HttpStatusCode.BadRequest, "Bad request", "Lỗi tải file dữ liệu trên hệ thống!");
                    }
                }
            } else
            {
                throw new ErrorException((int)HttpStatusCode.NotFound, "Not found", "File dữ liệu không tồn tại!");
            }
        }
    }
}
