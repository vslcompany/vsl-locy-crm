using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly VslDbContext _db;

        public ProfileService(VslDbContext db)
        {
            _db = db;            
        }

        public async Task<ProfileDto?> GetProfileById(long id)
        {
            ProfileDto? data = await _db.TblSysUsers.Where(x => x.Id == id).Select(x => new ProfileDto()
            {
                id = x.Id,
                username = x.UserName ?? "",
                active = x.Active,
                permission = x.Permission ?? "",
                idNhanVien = x.IdnhanVien,
                idChucVu = x.IdnhanVienNavigation != null ? x.IdnhanVienNavigation.IdchucVu : 0,
                idPhongBan = x.IdnhanVienNavigation != null ? x.IdnhanVienNavigation.IdphongBan : 0,
                idVanPhong = x.IdnhanVienNavigation != null ? x.IdnhanVienNavigation.IdvanPhong : 0,
                manhanvien = x.IdnhanVienNavigation != null ? x.IdnhanVienNavigation.Manhansu : "",
                chucVu = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IdchucVuNavigation != null) ? x.IdnhanVienNavigation.IdchucVuNavigation.NameVI : "",
                phongban = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IdphongBanNavigation != null) ? x.IdnhanVienNavigation.IdphongBanNavigation.NameVI : "",
                vanPhong = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IdvanPhongNavigation != null) ? x.IdnhanVienNavigation.IdvanPhongNavigation.NameVI : "",
                hoTenVI = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.HoTenVI != null) ? x.IdnhanVienNavigation.HoTenVI : "admin",
                hoTenEN = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.HoTenEN != null) ? x.IdnhanVienNavigation.HoTenEN : "admin",
                namsinh = x.IdnhanVienNavigation != null ? string.Format("{0:yyyy-MM-dd}", x.IdnhanVienNavigation.NamSinh) : "",
                gioitinh = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.GioiTinh != null) ? x.IdnhanVienNavigation.GioiTinh : 3,
                quequan = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.QueQuan != null) ? x.IdnhanVienNavigation.QueQuan : "",
                diachi = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.DiaChiHienTai != null) ? x.IdnhanVienNavigation.DiaChiHienTai : "",
                didong = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.DiDong != null) ? x.IdnhanVienNavigation.DiDong : "",
                email = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.Email != null) ? x.IdnhanVienNavigation.Email : "",
                soCMT = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.SoCmt != null) ? x.IdnhanVienNavigation.SoCmt : "",
                noiCapCMT = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.NoiCapCmt != null) ? x.IdnhanVienNavigation.NoiCapCmt : "",
                ngayCapCMT = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.NgayCapCmt != null) ? string.Format("{0:yyyy-MM-dd}", x.IdnhanVienNavigation.NgayCapCmt) : "",
                photoURL = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.PhotoUrl != null) ? x.IdnhanVienNavigation.PhotoUrl : "",
                ghichu = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.GhiChu != null) ? x.IdnhanVienNavigation.GhiChu : "",
                createDate = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.CreateDate != null) ? string.Format("{0:yyyy-MM-dd}", x.IdnhanVienNavigation.CreateDate) : "",
                editDate = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.EditDate != null) ? string.Format("{0:yyyy-MM-dd}", x.IdnhanVienNavigation.EditDate) : "",
                soLuongKH = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.SoLuongKh != null) ? x.IdnhanVienNavigation.SoLuongKh : 0,
                flagDelete = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.FlagDelete != null) ? x.IdnhanVienNavigation.FlagDelete : false,
                idUserDelete = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.IduserDelete != null) ? x.IdnhanVienNavigation.IduserDelete : 0,
                dateDelete = (x.IdnhanVienNavigation != null && x.IdnhanVienNavigation.DateDelete != null) ? string.Format("{0:yyyy-MM-dd}", x.IdnhanVienNavigation.DateDelete) : "",
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task UpdateProfile(TblNhanSu data, UpdateProfileRequest req)
        {
            data.HoTenVI = req.hoTenVI?.Trim()?.Length > 0 ? req.hoTenVI : data.HoTenVI;
            data.NamSinh = req.namsinh?.Trim()?.Length > 0 ? DateOnly.Parse(req.namsinh) : data.NamSinh;
            data.GioiTinh = req.gioitinh ?? data.GioiTinh;
            data.QueQuan = req.quequan?.Trim()?.Length > 0 ? req.quequan : data.QueQuan;
            data.DiaChiHienTai = req.diachi?.Trim()?.Length > 0 ? req.diachi : data.DiaChiHienTai;
            data.SoCmt = req.soCMT?.Trim()?.Length > 0 ? req.soCMT : data.SoCmt;
            data.NoiCapCmt = req.noiCapCMT?.Trim()?.Length > 0 ? req.noiCapCMT : data.NoiCapCmt;
            data.NgayCapCmt = req.ngayCapCMT?.Trim()?.Length > 0 ? DateOnly.Parse(req.ngayCapCMT) : data.NgayCapCmt;
            data.DiDong = req.didong?.Trim()?.Length > 0 ? req.didong : data.DiDong;
            data.Email = req.email?.Trim()?.Length > 0 ? req.email : data.Email;
            data.PhotoUrl = req.photoURL?.Trim().Length > 0 ? req.photoURL : data.PhotoUrl;

            await _db.SaveChangesAsync();
        }
    }
}
