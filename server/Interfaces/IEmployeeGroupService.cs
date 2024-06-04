using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Interfaces
{
    public interface IEmployeeGroupService
    {
        Task<List<EmployeeDto>?> GetEmployeeNoGroup();
        Task<List<EmployeeGroupDto>?> GetEmployeeGroups();
        Task<int> GetTotalEmployeeGroups();
        Task CreateGroup(CreateEmployeeGroupRequest req);
        Task UpdateGroup(UpdateEmployeeGroupRequest req);
        Task DeleteGroup(TblNhanSuTreelist data);
        Task<TblNhanSuTreelist?> GetEmployeeGroupById(long id);
    }
}
