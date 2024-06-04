using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Interfaces
{
    public interface IEmployeeService
    {
        // Manage data
        Task<QueryResponse<ProfileDto>> GetData(EmployeeQuery query);
        Task<TblSysUser?> GetAccountById(long id);
        Task<TblNhanSu?> GetEmployeeById(long id);
        Task Create(CreateEmployeeRequest req);
        Task Update(TblSysUser? account, TblNhanSu info, UpdateEmployeeRequest req);
        Task Delete(TblNhanSu data, DeleteEmployeeRequest req);
        Task<bool> IsPersonnelCodeExist(string manhanvien);
        Task<bool> IsUsernameExist(string username);

        // Group
        Task<List<long>> GetListEmployee(long idNhanVien);
        Task<List<EmployeeDto>?> GetAllEmployees();
        Task<List<EmployeeDto>?> GetAllEmployeesGroup(List<long> ids);
    }
}
