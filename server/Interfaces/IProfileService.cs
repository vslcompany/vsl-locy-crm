using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto?> GetProfileById(long id);
        Task UpdateProfile(TblNhanSu data, UpdateProfileRequest req);
    }
}
