using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly VslDbContext _db;
        private readonly IAuthService<TblSysUser> _authService;

        public EmployeeService(VslDbContext db, IAuthService<TblSysUser> authService)
        {
            _db = db;
            _authService = authService;
        }

        private async Task<QueryResponse<ProfileDto>> Query(EmployeeQuery query)
        {
            var q = _db.TblNhanSus.AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(query.Name))
            {
                var text = query.Name.ToLower().Trim();
                q = q.Where(x => x.HoTenVI.ToLower().Contains(text));
            }

            if(query.IdChucVu != null)
            {
                q = q.Where(x => x.IdchucVu == query.IdChucVu);
            }

            if (query.IdVanPhong != null)
            {
                q = q.Where(x => x.IdvanPhong == query.IdVanPhong);
            }

            if (query.IdPhongBan != null)
            {
                q = q.Where(x => x.IdphongBan == query.IdPhongBan);
            }

            if(query.TrangThai)
            {
                q = q.Where(x => x.FlagDelete != true);
            } else
            {
                q = q.Where(x => x.FlagDelete == true);
            }

            // Get data
            var total = await q.CountAsync();
            var data = await q.Select(e => new ProfileDto()
            {
                id = _db.TblSysUsers.Where(x => x.IdnhanVien == e.Id).Select(x => x.Id).FirstOrDefault(),
                username = _db.TblSysUsers.Where(x => x.IdnhanVien == e.Id).Select(x => x.UserName).FirstOrDefault() ?? "",
                active = _db.TblSysUsers.Where(x => x.IdnhanVien == e.Id).Select(x => x.Active).FirstOrDefault() ?? false,
                permission = _db.TblSysUsers.Where(x => x.IdnhanVien == e.Id).Select(x => x.Permission).FirstOrDefault() ?? "",
                idNhanVien = e.Id,
                idChucVu = e.IdchucVu,
                idPhongBan = e.IdphongBan,
                idVanPhong = e.IdvanPhong,
                manhanvien = e.Manhansu,
                chucVu = e.IdchucVuNavigation != null ? (e.IdchucVuNavigation.NameVI ?? "") : "",
                phongban = e.IdphongBanNavigation != null ? (e.IdphongBanNavigation.NameVI ?? "") : "",
                vanPhong = e.IdvanPhongNavigation != null ? (e.IdvanPhongNavigation.NameVI ?? "") : "",
                hoTenVI = e.HoTenVI ?? "",
                hoTenEN = e.HoTenEN ?? "",
                namsinh = string.Format("{0:yyyy-MM-dd}", e.NamSinh),
                gioitinh = e.GioiTinh ?? 3,
                quequan = e.QueQuan ?? "",
                diachi = e.DiaChiHienTai ?? "",
                soCMT = e.SoCmt ?? "",
                noiCapCMT = e.NoiCapCmt ?? "",
                photoURL = e.PhotoUrl ?? "",
                didong = e.DiDong ?? "",
                ghichu = e.GhiChu ?? "",
                soLuongKH = e.SoLuongKh ?? 0,
                email = e.Email ?? "",
                flagDelete = e.FlagDelete ?? false,
                ngayCapCMT = e.NgayCapCmt != null ? string.Format("{0:yyyy-MM-dd}", e.NgayCapCmt) : "",
                idUserDelete = e.IduserDelete,
                createDate = e.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", e.CreateDate) : "",
                editDate = e.EditDate != null ? string.Format("{0:yyyy-MM-dd}", e.EditDate) : "",
                dateDelete = e.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", e.DateDelete) : ""
            }).OrderByDescending(c => c.id).Skip(query.Start).Take(query.Size).ToListAsync();
            

            var result = new QueryResponse<ProfileDto>()
            {
                data = data,
                totalRow = total,
            };

            return result;
        }

        private void GetEmployeeView(TblNhanSuTreelist c, List<TblNhanSuTreelist> ListEmployeeTreeList, List<long> listEmployee)
        {
            if (c == null) return;

            var ListEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == c.Id).ToList();

            if (ListEmployeeParent.Count > 0) // Trưởng nhóm
            {
                foreach (var employee in ListEmployeeParent)
                {
                    if (employee.IdnhanVien != null)
                    {
                        listEmployee.Add(employee.IdnhanVien.Value);
                    }

                    var _listEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == employee.Id).ToList();
                    if (_listEmployeeParent.Count > 0)
                    {
                        GetEmployeeView(employee, ListEmployeeTreeList, listEmployee);
                    }
                }
            }
        }

        public async Task Create(CreateEmployeeRequest req)
        {
            // Add employee data to db
            var data = new TblNhanSu()
            {
                Manhansu = req.manhanvien,
                HoTenVI = req.HoTenVI,
                IdchucVu = req.IdChucVu != null ? req.IdChucVu : null,
                IdphongBan = req.IdPhongBan != null ? req.IdPhongBan : null,
                IdvanPhong = req.IdVanPhong != null ? req.IdVanPhong : null,
                GioiTinh = req.GioiTinh != null ? req.GioiTinh : null,
                QueQuan = req.QueQuan ?? "",
                DiaChiHienTai = req.DiaChi ?? "",
                SoCmt = req.SoCMT ?? "",
                NoiCapCmt = req.NoiCapCMT ?? "",
                PhotoUrl = req.PhotoURL ?? "",
                GhiChu = req.GhiChu ?? "",
                FlagDelete = false,
                NgayCapCmt = (req.NgayCapCMT != null && req.NgayCapCMT != "") ? DateOnly.Parse(req.NgayCapCMT) : null,
                SoLuongKh = req.SoLuongKH ?? 0,
                HoTenEN = req.HoTenEN ?? "",
                CreateDate = DateTime.Now
            };

            if(req.NamSinh != null && req.NamSinh != "")
            {
                data.NamSinh = DateOnly.Parse(req.NamSinh);
            }

            await _db.TblNhanSus.AddAsync(data);
            await _db.SaveChangesAsync();

            if (req.Username != null && req.Username != "" && req.Password != null && req.Password != "")
            {
                // Add user data to db with id employee
                var hashPassword = _authService.hashPassword(req.Password);

                var account = new TblSysUser()
                {
                    UserName = req.Username,
                    Password = hashPassword,
                    Permission = req.Permission,
                    Active = true,
                    IdnhanVien = data.Id,
                };

                await _db.TblSysUsers.AddAsync(account);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete(TblNhanSu data, DeleteEmployeeRequest req)
        {
            data.FlagDelete = req.FlagDelete;
            data.IduserDelete = req.IdUserDelete;
            data.DateDelete = req.FlagDelete == true ? DateTime.Now : null;

            await _db.SaveChangesAsync();
        }

        public async Task<TblSysUser?> GetAccountById(long id)
        {
            TblSysUser? data = await _db.TblSysUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<EmployeeDto>?> GetAllEmployees()
        {
            List<EmployeeDto>? data = await _db.TblNhanSus.Where(x => x.FlagDelete != true).Select(x => new EmployeeDto()
            {
                Id = x.Id,
                NameVI = x.HoTenVI ?? "",
                NameEN = x.HoTenEN ?? "",
            }).ToListAsync();
            return data;
        }

        public async Task<List<EmployeeDto>?> GetAllEmployeesGroup(List<long> ids)
        {
            List<EmployeeDto>? dataList = await _db.TblNhanSus.AsNoTracking().Where(x => x.FlagDelete != true)
                                               .Select(x => new EmployeeDto()
                                               {
                                                   Id = x.Id,
                                                   NameVI = x.HoTenVI ?? "",
                                                   NameEN = x.HoTenEN ?? "",
                                               })
                                               .ToListAsync();
            var data = dataList.Where(x => ids != null && ids.Contains(x.Id)).ToList();
            return data;
        }

        public async Task<QueryResponse<ProfileDto>> GetData(EmployeeQuery query)
        {
            var data = await Query(query);
            return data;
        }

        public async Task<TblNhanSu?> GetEmployeeById(long id)
        {
            TblNhanSu? data = await _db.TblNhanSus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<long>> GetListEmployee(long idNhanVien)
        {
            List<long> employees = new List<long>();

            var ListEmployeeTreeList = await _db.TblNhanSuTreelists.Where(x => x.IdnhanVien != null).ToListAsync();

            if (ListEmployeeTreeList.Count > 0)
            {
                var EmployeeTreeList = ListEmployeeTreeList.FirstOrDefault(x => x.IdnhanVien == idNhanVien);

                if (EmployeeTreeList != null)
                {
                    var ListEmployeeParent = ListEmployeeTreeList.Where(x => x.ParentId == EmployeeTreeList.Id).ToList();

                    if (ListEmployeeParent.Count > 0) // Trưởng nhóm
                    {
                        GetEmployeeView(EmployeeTreeList, ListEmployeeTreeList, employees);
                    }
                }
            }

            return employees;
        }

        public async Task<bool> IsPersonnelCodeExist(string manhanvien)
        {
            var data = await _db.TblNhanSus.Where(x => x.Manhansu == manhanvien).FirstOrDefaultAsync();
            if (data == null) return false;
            return true;
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            var data = await _db.TblSysUsers.Where(x => x.UserName == username).FirstOrDefaultAsync();
            if (data == null) return false;
            return true;
        }

        public async Task Update(TblSysUser? account, TblNhanSu info, UpdateEmployeeRequest req)
        {
            if (account != null)
            {
                account.UserName = req.Username != null && req.Username != "" ? req.Username : account.UserName;
                if (req.Password != "" && req.Password != null)
                {
                    var hashPassword = _authService.hashPassword(req.Password);
                    account.Password = hashPassword;
                }
                account.Permission = req.Permission ?? account.Permission;
                account.Active = req.Active ?? account.Active;
            }

            if (account == null && req.Username != null && req.Username != "" && req.Password != null && req.Password != "")
            {
                // Add user data to db with id employee
                var hashPassword = _authService.hashPassword(req.Password);

                var newAccount = new TblSysUser()
                {
                    UserName = req.Username,
                    Password = hashPassword,
                    Permission = req.Permission,
                    Active = true,
                    IdnhanVien = info.Id
                };

                await _db.TblSysUsers.AddAsync(newAccount);
                await _db.SaveChangesAsync();
            }

            info.IdchucVu = req.IdChucVu != -1 ? req.IdChucVu : null;
            info.IdphongBan = req.IdPhongBan != -1 ? req.IdPhongBan : null;
            info.IdvanPhong = req.IdVanPhong != -1 ? req.IdVanPhong : null;
            info.Manhansu = req.manhanvien ?? info.Manhansu;
            info.HoTenVI = req.HoTenVI ?? info.HoTenVI;
            info.HoTenEN = req.HoTenEN ?? info.HoTenEN;
            if (req.NamSinh != null && req.NamSinh != "")
            {
                info.NamSinh = DateOnly.Parse(req.NamSinh);
            }
            info.GioiTinh = req.GioiTinh != -1 ? req.GioiTinh : null;
            info.QueQuan = req.QueQuan ?? info.QueQuan;
            info.DiaChiHienTai = req.DiaChi ?? info.DiaChiHienTai;
            info.SoCmt = req.SoCMT ?? info.SoCmt;
            info.NoiCapCmt = req.NoiCapCMT ?? info.NoiCapCmt;
            if (req.NgayCapCMT != null && req.NgayCapCMT != "")
            {
                info.NgayCapCmt = DateOnly.Parse(req.NgayCapCMT);
            }
            info.PhotoUrl = req.PhotoURL ?? info.PhotoUrl;
            info.GhiChu = req.GhiChu ?? info.GhiChu;
            info.SoLuongKh = req.SoLuongKH ?? info.SoLuongKh;
            info.EditDate = DateTime.Now;

            await _db.SaveChangesAsync();
        }
    }
}
