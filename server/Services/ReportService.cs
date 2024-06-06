using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Services
{
    public class ReportService : IReportService
    {
        private readonly VslDbContext _db;

        public ReportService(VslDbContext db)
        {
            _db = db;
        }

        private async Task<QueryResponse<ReportWorkDto>> Query(ReportWorkQuery query, string role, long idUser, List<long> idEmployees)
        {
            var q = _db.TblCustomerTacNghieps.AsQueryable().AsNoTracking();

            q = q.Where(x => (x.ThoiGianThucHien != null && x.ThoiGianThucHien >= DateTime.Parse(query.StartDate) && x.ThoiGianThucHien <= DateTime.Parse(query.EndDate)) ||
            (x.ThoiGianThucHien == null && x.DateCreate != null && x.DateCreate >= DateTime.Parse(query.StartDate) && x.DateCreate <= DateTime.Parse(query.EndDate)));

            List<ReportWorkDto>? data = new List<ReportWorkDto>();
            var total = 0;

            if (role == "admin")
            {
                total = await q.CountAsync();
                data = await q.Select(x => new ReportWorkDto()
                {
                    NhanVienTao = x.IduserCreateNavigation != null && x.IduserCreateNavigation.IdnhanVienNavigation != null ? (x.IduserCreateNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "admin",
                    NhanVien = x.IddmcustomerNavigation != null && x.IddmcustomerNavigation.IdnhanVienSaleNavigation != null ? x.IddmcustomerNavigation.IdnhanVienSaleNavigation.HoTenVI : "",
                    KhachHang = x.IddmcustomerNavigation != null ? x.IddmcustomerNavigation.NameVI ?? "" : "",
                    LoaiTacNghiep = x.IdloaiTacNghiepNavigation != null ? x.IdloaiTacNghiepNavigation.Name ?? "" : "",
                    NguoiLienHe = x.IdnguoiLienHeNavigation != null ? x.IdnguoiLienHeNavigation.NameVI ?? "" : "",
                    NoiDung = x.NoiDung,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    KhachHangPhanHoi = x.KhachHangPhanHoi ?? "",
                    ThoiGianPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : ""
                }).ToListAsync();
            }

            if (role == "manager")
            {
                var resultFilter = await q.Include(x => x.IddmcustomerNavigation).Include(x => x.IduserCreateNavigation).ThenInclude(x => x.IdnhanVienNavigation)
                                          .Include(x => x.IdnguoiLienHeNavigation).Include(x => x.IdloaiTacNghiepNavigation).ToListAsync();
                total = resultFilter.Where(x => (x.IddmcustomerNavigation != null && x.IddmcustomerNavigation.IdnhanVienSale != null && idEmployees.Contains(x.IddmcustomerNavigation.IdnhanVienSale.Value)) || x.IduserCreate == idUser).Count();
                data = resultFilter.Where(x => (x.IddmcustomerNavigation != null && x.IddmcustomerNavigation.IdnhanVienSale != null && idEmployees.Contains(x.IddmcustomerNavigation.IdnhanVienSale.Value)) || x.IduserCreate == idUser).Select(x => new ReportWorkDto()
                {
                    NhanVienTao = x.IduserCreateNavigation != null && x.IduserCreateNavigation.IdnhanVienNavigation != null ? (x.IduserCreateNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "admin",
                    NhanVien = x.IddmcustomerNavigation != null && x.IddmcustomerNavigation.IdnhanVienSaleNavigation != null ? x.IddmcustomerNavigation.IdnhanVienSaleNavigation.HoTenVI : "",
                    KhachHang = x.IddmcustomerNavigation != null ? x.IddmcustomerNavigation.NameVI ?? "" : "",
                    LoaiTacNghiep = x.IdloaiTacNghiepNavigation != null ? x.IdloaiTacNghiepNavigation.Name ?? "" : "",
                    NguoiLienHe = x.IdnguoiLienHeNavigation != null ? x.IdnguoiLienHeNavigation.NameVI ?? "" : "",
                    NoiDung = x.NoiDung,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    KhachHangPhanHoi = x.KhachHangPhanHoi ?? "",
                    ThoiGianPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : ""
                }).ToList();
            }

            if (role == "employee")
            {
                total = await q.Where(x => x.IduserCreate == idUser).CountAsync();
                data = await q.Where(x => x.IduserCreate == idUser).Select(x => new ReportWorkDto()
                {
                    NhanVienTao = x.IduserCreateNavigation != null && x.IduserCreateNavigation.IdnhanVienNavigation != null ? (x.IduserCreateNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "admin",
                    NhanVien = x.IddmcustomerNavigation != null && x.IddmcustomerNavigation.IdnhanVienSaleNavigation != null ? x.IddmcustomerNavigation.IdnhanVienSaleNavigation.HoTenVI : "",
                    KhachHang = x.IddmcustomerNavigation != null ? x.IddmcustomerNavigation.NameVI ?? "" : "",
                    LoaiTacNghiep = x.IdloaiTacNghiepNavigation != null ? x.IdloaiTacNghiepNavigation.Name ?? "" : "",
                    NguoiLienHe = x.IdnguoiLienHeNavigation != null ? x.IdnguoiLienHeNavigation.NameVI ?? "" : "",
                    NoiDung = x.NoiDung,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    KhachHangPhanHoi = x.KhachHangPhanHoi ?? "",
                    ThoiGianPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : ""
                }).ToListAsync();
            }

            var result = new QueryResponse<ReportWorkDto>()
            {
                data = data,
                totalRow = total,
            };

            return result;
        }

        public async Task<QueryResponse<ReportWorkDto>> GetReportWork(ReportWorkQuery query, string permission, long idUser, List<long> idEmployees)
        {
            string role;
            if (permission.Contains("1048576") || permission.Contains("7000"))
            {
                role = "admin";
            }
            else if (permission.Contains("7080"))
            {
                role = "manager";
            }
            else
            {
                role = "employee";
            }
            var data = await Query(query, role, idUser, idEmployees);
            return data;
        }
    }
}
