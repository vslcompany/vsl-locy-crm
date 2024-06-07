using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Interfaces
{
    public interface ICustomerInfoService
    {
        // Thông tin chi tiết
        Task<CustomerDto?> GetCustomersById(long id);
        // Xuất nhập khẩu
        Task<List<CustomerListImExDto>?> GetCustomerListImEx(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerListImEx(string Search = "", long idCustomer = 0);
        Task<TblCustomerListImEx?> GetCustomerImExById(long id);
        Task CreateCustomerImEx(CreateCustomerImExRequest req);
        Task UpdateCustomerImEx(TblCustomerListImEx data, UpdateCustomerImExRequest req);
        Task DeleteCustomerImEx(TblCustomerListImEx data);
        // Tác nghiệp
        Task<List<CustomerOperationalDto>?> GetCustomerOperationals(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerOperationals(string Search = "", long idCustomer = 0);
        Task<TblCustomerTacNghiep?> GetCustomerOperationalById(long id);
        Task CreateCustomerOperational(CreateCustomerOperationalRequest req, TblDmcustomer? customer);
        Task UpdateCustomerOperational(TblCustomerTacNghiep data, UpdateCustomerOperationalRequest req);
        Task DeleteCustomerOperational(TblCustomerTacNghiep data);
        // Người liên hệ
        Task<List<CustomerContactDto>?> GetAllCustomerContacts(long idCustomer = 0);
        Task<List<CustomerContactDto>?> GetCustomerContacts(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerContacts(string Search = "", long idCustomer = 0);
        Task<TblDmcontactList?> GetCustomerContactById(long id);
        Task CreateCustomerContact(CreateCustomerContactRequest req);
        Task UpdateCustomerContact(TblDmcontactList data, UpdateCustomerContactRequest req);
        Task DeleteCustomerContact(TblDmcontactList data);
        // Đánh giá
        Task<List<CustomerEvaluateDto>?> GetCustomerEvaluates(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerEvaluates(string Search = "", long idCustomer = 0);
        Task<TblDmcustomerDanhGium?> GetCustomerEvaluateById(long id);
        Task CreateCustomerEvaluate(CreateCustomerEvaluateRequest req);
        Task UpdateCustomerEvaluate(TblDmcustomerDanhGium data, UpdateCustomerEvaluateRequest req);
        Task DeleteCustomerEvaluate(TblDmcustomerDanhGium data);
        // Phân loại khách hàng
        Task<List<CustomerClassifyDto>?> GetCustomerClassifies(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerClassifies(string Search = "", long idCustomer = 0);
        Task<TblDmcustomerPhanLoaiKh?> GetCustomerClassifyById(long id);
        Task CreateCustomerClassify(CreateCustomerClassifyRequest req);
        Task UpdateCustomerClassify(TblDmcustomerPhanLoaiKh data, UpdateCustomerClassifyRequest req);
        Task DeleteCustomerClassify(TblDmcustomerPhanLoaiKh data);
        // Nghiệp vụ
        Task<List<CustomerMajorDto>?> GetCustomerMajors(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerMajors(string Search = "", long idCustomer = 0);
        Task<TblDmcustomerNghiepVu?> GetCustomerMajorById(long id);
        Task CreateCustomerMajor(CreateCustomerMajorRequest req);
        Task UpdateCustomerMajor(TblDmcustomerNghiepVu data, UpdateCustomerMajorRequest req);
        Task DeleteCustomerMajor(TblDmcustomerNghiepVu data);
        // Tuyến hàng
        Task<List<CustomerRouteDto>?> GetCustomerRoutes(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0);
        Task<int> GetTotalCustomerRoutes(string Search = "", long idCustomer = 0);
        Task<TblDmcustomerTuyenHang?> GetCustomerRouteById(long id);
        Task CreateCustomerRoute(CreateCustomerRouteRequest req);
        Task UpdateCustomerRoute(TblDmcustomerTuyenHang data, UpdateCustomerRouteRequest req);
        Task DeleteCustomerRoute(TblDmcustomerTuyenHang data);
    }
}
