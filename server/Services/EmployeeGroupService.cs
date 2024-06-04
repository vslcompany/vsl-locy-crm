using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Services
{
    public class EmployeeGroupService: IEmployeeGroupService
    {
        private readonly VslDbContext _db;

        public EmployeeGroupService(VslDbContext db)
        {
            _db = db;
        }

        public async Task CreateGroup(CreateEmployeeGroupRequest req)
        {
            foreach (var item in req.IdNhanVien)
            {
                var newItem = new TblNhanSuTreelist()
                {
                    ParentId = req.ParentId != -1 ? req.ParentId : 0,
                    NameGroup = req.NameGroup,
                    IdnhanVien = item,
                    FlagViewAllGroup = false,
                };

                await _db.TblNhanSuTreelists.AddAsync(newItem);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteGroup(TblNhanSuTreelist data)
        {
            _db.TblNhanSuTreelists.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<TblNhanSuTreelist?> GetEmployeeGroupById(long id)
        {
            TblNhanSuTreelist? data = await _db.TblNhanSuTreelists.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<EmployeeGroupDto>?> GetEmployeeGroups()
        {
            List<EmployeeGroupDto>? data = await _db.TblNhanSuTreelists.Select(x => new EmployeeGroupDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                NameGroup = x.NameGroup,
                FlagViewAllGroup = x.FlagViewAllGroup ?? false,
                IdNhanVien = x.IdnhanVien,
                NameVI = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.HoTenVI != null) ? x.IdnhanVienNavigation.HoTenVI : "",
                ChucVu = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IdchucVuNavigation != null && x.IdnhanVienNavigation.IdchucVuNavigation.NameVI != null) ? x.IdnhanVienNavigation.IdchucVuNavigation.NameVI : "",
            }).Where(x => x.ParentId == 0 || _db.TblNhanSuTreelists.Any(y => y.Id == x.ParentId)).ToListAsync();

            return data;
        }

        public async Task<List<EmployeeDto>?> GetEmployeeNoGroup()
        {
            List<EmployeeDto>? data = await _db.TblNhanSus.Where(x => x.FlagDelete != true).Select(x => new EmployeeDto()
            {
                Id = x.Id,
                NameVI = x.HoTenVI ?? "",
                NameEN = x.HoTenEN ?? "",
            }).Where(x => !_db.TblNhanSuTreelists.Any(y => y.IdnhanVien == x.Id)).ToListAsync();

            return data;
        }

        public async Task<int> GetTotalEmployeeGroups()
        {
            var total = await _db.TblNhanSuTreelists.Select(x => new EmployeeGroupDto()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                NameGroup = x.NameGroup,
                FlagViewAllGroup = x.FlagViewAllGroup ?? false,
                IdNhanVien = x.IdnhanVien,
                NameVI = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.HoTenVI != null) ? x.IdnhanVienNavigation.HoTenVI : "",
                ChucVu = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IdchucVuNavigation != null && x.IdnhanVienNavigation.IdchucVuNavigation.NameVI != null) ? x.IdnhanVienNavigation.IdchucVuNavigation.NameVI : "",
            }).Where(x => x.ParentId == 0 || _db.TblNhanSuTreelists.Any(y => y.Id == x.ParentId)).CountAsync();

            return total;
        }

        public Task UpdateGroup(UpdateEmployeeGroupRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
