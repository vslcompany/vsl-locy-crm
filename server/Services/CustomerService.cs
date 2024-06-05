using Microsoft.EntityFrameworkCore;
using System.Data;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly VslDbContext _db;

        public CustomerService(VslDbContext db)
        {
            _db = db;
        }

        private async Task<QueryResponse<CustomerDto>> Query(CustomerQuery query, string role, long idUser, long idEmployee, List<long> idEmployees)
        {
            var q = _db.TblDmcustomers.AsQueryable().AsNoTracking();

            // Customer condition
            if (query.IDLoaiDoanhNghiep != null)
            {
                q = q.Where(c => c.IdloaiDoanhNghiep == query.IDLoaiDoanhNghiep);
            }

            if (query.IDNghiepVu != null)
            {
                q = q.Where(c => _db.TblDmcustomerNghiepVus.Any(x => x.Iddmcustomer == c.Id && x.IddmnghiepVu == query.IDNghiepVu));
            }

            if (query.IDPhanLoaiKhachHang != null)
            {
                q = q.Where(c => _db.TblDmcustomerPhanLoaiKhs.Any(x => x.Iddmcustomer == c.Id && x.IddmphanLoaiKhachHang == query.IDPhanLoaiKhachHang));
            }

            if (query.IDDanhGia != null)
            {
                q = q.Where(c => _db.TblDmcustomerDanhGia.Any(x => x.Iddmcustomer == c.Id && x.IddmcustomerType == query.IDDanhGia));
            }

            if (query.IDLoaiTacNghiep != null)
            {
                q = q.Where(c => _db.TblCustomerTacNghieps.Any(x => x.Iddmcustomer == c.Id && x.IdloaiTacNghiep == query.IDLoaiTacNghiep));
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                var text = query.Name.ToLower().Trim();
                q = q.Where(c => c.NameVI != null && c.NameVI.ToLower().Contains(text));
            }

            if (!string.IsNullOrEmpty(query.TaxCode))
            {
                var text = query.TaxCode.ToLower().Trim();
                q = q.Where(c => c.TaxCode != null && c.TaxCode.ToLower().Contains(text));
            }

            // Route condition
            if (query.IDQuocGiaDiTuyenHang != null)
            {
                q = q.Where(c => _db.TblDmcustomerTuyenHangs.Any(x => x.Iddmcustomer == c.Id && x.IdquocGiaDi == query.IDQuocGiaDiTuyenHang));
            }

            if (query.IDQuocGiaDenTuyenHang != null)
            {
                q = q.Where(c => _db.TblDmcustomerTuyenHangs.Any(x => x.Iddmcustomer == c.Id && x.IdquocGiaDen == query.IDQuocGiaDenTuyenHang));
            }

            if (query.IDCangDiTuyenHang != null)
            {
                q = q.Where(c => _db.TblDmcustomerTuyenHangs.Any(x => x.Iddmcustomer == c.Id && x.IdcangDi == query.IDCangDiTuyenHang));
            }

            if (query.IDCangDenTuyenHang != null)
            {
                q = q.Where(c => _db.TblDmcustomerTuyenHangs.Any(x => x.Iddmcustomer == c.Id && x.IdcangDen == query.IDCangDenTuyenHang));
            }

            // ImEx condition
            if (query.IDQuocGiaDiXNK != null)
            {
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.IdfromCountry == query.IDQuocGiaDiXNK));
            }

            if (query.IDQuocGiaDenXNK != null)
            {
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.IdtoCountry == query.IDQuocGiaDenXNK));
            }

            if (query.IDCangDiXNK != null)
            {
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.IdfromPort == query.IDCangDiXNK));
            }

            if (query.IDCangDenXNK != null)
            {
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.IdtoPort == query.IDCangDenXNK));
            }

            if (!string.IsNullOrEmpty(query.Term))
            {
                var text = query.Term.ToLower().Trim();
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.Term != null &&
                                         x.Term.ToLower().Contains(text)));
            }

            if (!string.IsNullOrEmpty(query.HSCode))
            {
                var text = query.HSCode.ToLower().Trim();
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.Code != null &&
                                         x.Code.ToLower().Contains(text)));
            }

            if (!string.IsNullOrEmpty(query.Type))
            {
                var text = query.Type.ToLower().Trim();
                q = q.Where(c => _db.TblCustomerListImExes.Any(x => x.Iddmcustomer == c.Id && x.Type != null &&
                                         x.Type.ToLower().Contains(text)));
            }

            // List type condition
            if (query.ListType == "delete")
            {
                q = q.Where(c => c.FlagDel == true);
            }
            else
            {
                q = q.Where(c => c.FlagDel != true);
            }

            if (query.ListType == "all")
            {
                q = ApplyAllCondition(q, role, idEmployee);
            }

            if (query.ListType == "assigned")
            {
                q = ApplyAssignedCondition(q, role, idUser, idEmployee);
            }

            if (query.ListType == "delivered")
            {
                q = ApplyDeliveredCondition(q, idEmployee);
            }

            if (query.ListType == "received")
            {
                q = ApplyReceivedCondition(q, role, idUser, idEmployee);
            }

            if (query.ListType == "undelivered")
            {
                q = ApplyUndeliveredCondition(q);
            }

            List<CustomerDto>? data = new List<CustomerDto>();
            var total = 0;

            if (!idEmployees.Any())
            {
                total = await q.CountAsync();
                data = await q.Select(c => new CustomerDto()
                {
                    Id = c.Id,
                    IdQuocGia = c.IdquocGia,
                    IdCity = c.Idcity,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    AddressVI = c.AddressVI ?? "",
                    AddressEN = c.AddressEN ?? "",
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (c.IdcityNavigation != null && c.IdcityNavigation.NameVI != null) ? c.IdcityNavigation.NameVI : "",
                    Code = c.Code ?? "",
                    TaxCode = c.TaxCode ?? "",
                    Phone = c.Phone ?? "",
                    Fax = c.Fax ?? "",
                    Email = c.Email ?? "",
                    Website = c.Website ?? "",
                    Note = c.Note ?? "",
                    IdNhanVienSale = c.IdnhanVienSale,
                    IdUserCreate = c.IduserCreate,
                    NhanVien = (c.IdnhanVienSaleNavigation != null && c.IdnhanVienSaleNavigation.HoTenVI != null) ? c.IdnhanVienSaleNavigation.HoTenVI : "",
                    NguoiTao = (c.IduserCreateNavigation != null && c.IduserCreateNavigation.IdnhanVienNavigation != null) ? (c.IduserCreateNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "",
                    FlagActive = c.FlagActive ?? true,
                    FlagDel = c.FlagDel ?? false,
                    EnumGiaoNhan = c.EnumGiaoNhan ?? 0,
                    EnumLoaiKhachHang = c.EnumLoaiKhachHang ?? 0,
                    IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                    IdUserDelete = c.IduserDelete,
                    LoaiDoanhNghiep = (c.IdloaiDoanhNghiepNavigation != null) ? (c.IdloaiDoanhNghiepNavigation.NameVI ?? "") : "",
                    NguoiXoa = (c.IduserDeleteNavigation != null && c.IduserDeleteNavigation.IdnhanVienNavigation != null) ? (c.IduserDeleteNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "",
                    LyDoXoa = c.LyDoXoa ?? "",
                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "",
                    NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "",
                    NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                    NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                    SttMaxTacNghiep = c.SttmaxTacNghiep,
                    NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "",
                    NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                    IdUserGiaoViec = c.IduserGiaoViec,
                    IdUserTraKhach = c.IduserTraKhach,
                    ListTacNghiepText = c.ListTacNghiepText ?? "",
                    ListTuyenHangText = c.ListTuyenHangText ?? "",
                    ListPhanHoiText = c.ListPhanHoiText ?? "",
                    NguoiGiaoViec = (c.IduserGiaoViecNavigation != null && c.IduserGiaoViecNavigation.IdnhanVienNavigation != null) ? (c.IduserGiaoViecNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    NguoiTraKhach = (c.IduserTraKhachNavigation != null && c.IduserTraKhachNavigation.IdnhanVienNavigation != null) ? (c.IduserTraKhachNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                    NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "",
                    ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                    LyDoTuChoi = c.LyDoTuChoi ?? "",
                    IdTacNghiepCuoi = c.IdtacNghiepCuoi,
                    MauTacNghiepCuoi = c.MauTacNghiepCuoi ?? "",
                }).OrderByDescending(c => c.Id).Skip(query.Start).Take(query.Size).ToListAsync();
            }
            else
            {
                var resultFilter = await q.Select(c => new CustomerDto()
                {
                    Id = c.Id,
                    IdQuocGia = c.IdquocGia,
                    IdCity = c.Idcity,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    AddressVI = c.AddressVI ?? "",
                    AddressEN = c.AddressEN ?? "",
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (c.IdcityNavigation != null && c.IdcityNavigation.NameVI != null) ? c.IdcityNavigation.NameVI : "",
                    Code = c.Code ?? "",
                    TaxCode = c.TaxCode ?? "",
                    Phone = c.Phone ?? "",
                    Fax = c.Fax ?? "",
                    Email = c.Email ?? "",
                    Website = c.Website ?? "",
                    Note = c.Note ?? "",
                    IdNhanVienSale = c.IdnhanVienSale,
                    IdUserCreate = c.IduserCreate,
                    NhanVien = (c.IdnhanVienSaleNavigation != null && c.IdnhanVienSaleNavigation.HoTenVI != null) ? c.IdnhanVienSaleNavigation.HoTenVI : "",
                    NguoiTao = (c.IduserCreateNavigation != null && c.IduserCreateNavigation.IdnhanVienNavigation != null) ? (c.IduserCreateNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    DateCreate = c.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", c.DateCreate) : "",
                    FlagActive = c.FlagActive ?? true,
                    FlagDel = c.FlagDel ?? false,
                    EnumGiaoNhan = c.EnumGiaoNhan ?? 0,
                    EnumLoaiKhachHang = c.EnumLoaiKhachHang ?? 0,
                    IdLoaiDoanhNghiep = c.IdloaiDoanhNghiep,
                    IdUserDelete = c.IduserDelete,
                    LoaiDoanhNghiep = (c.IdloaiDoanhNghiepNavigation != null) ? (c.IdloaiDoanhNghiepNavigation.NameVI ?? "") : "",
                    NguoiXoa = (c.IduserDeleteNavigation != null && c.IduserDeleteNavigation.IdnhanVienNavigation != null) ? (c.IduserDeleteNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    DateDelete = c.DateDelete != null ? string.Format("{0:yyyy-MM-dd}", c.DateDelete) : "",
                    LyDoXoa = c.LyDoXoa ?? "",
                    NgayChonKhach = c.NgayChonKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChonKhach) : "",
                    NgayTraVe = c.NgayTraVe != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTraVe) : "",
                    NgayChotKhach = c.NgayChotKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayChotKhach) : "",
                    NgayTacNghiep = c.NgayTacNghiep != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTacNghiep) : "",
                    NgayTuongTac = c.NgayTuongTac != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuongTac) : "",
                    SttMaxTacNghiep = c.SttmaxTacNghiep,
                    NgayGiao = c.NgayGiao != null ? string.Format("{0:yyyy-MM-dd}", c.NgayGiao) : "",
                    NgayNhan = c.NgayNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayNhan) : "",
                    IdUserGiaoViec = c.IduserGiaoViec,
                    IdUserTraKhach = c.IduserTraKhach,
                    ListTacNghiepText = c.ListTacNghiepText ?? "",
                    ListTuyenHangText = c.ListTuyenHangText ?? "",
                    ListPhanHoiText = c.ListPhanHoiText ?? "",
                    NguoiGiaoViec = (c.IduserGiaoViecNavigation != null && c.IduserGiaoViecNavigation.IdnhanVienNavigation != null) ? (c.IduserGiaoViecNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    NguoiTraKhach = (c.IduserTraKhachNavigation != null && c.IduserTraKhachNavigation.IdnhanVienNavigation != null) ? (c.IduserTraKhachNavigation.IdnhanVienNavigation.HoTenVI ?? "") : "",
                    NgayTuTraKhach = c.NgayTuTraKhach != null ? string.Format("{0:yyyy-MM-dd}", c.NgayTuTraKhach) : "",
                    NgayKetThucNhan = c.NgayKetThucNhan != null ? string.Format("{0:yyyy-MM-dd}", c.NgayKetThucNhan) : "",
                    ThongTinGiaoViec = c.ThongTinGiaoViec ?? "",
                    LyDoTuChoi = c.LyDoTuChoi ?? "",
                    IdTacNghiepCuoi = c.IdtacNghiepCuoi,
                    MauTacNghiepCuoi = c.MauTacNghiepCuoi ?? "",
                }).ToListAsync();
                switch (query.ListType)
                {
                    case "all":
                        data = resultFilter.Where(c => c.IdNhanVienSale == null || c.IdNhanVienSale == idEmployee || (c.IdNhanVienSale != null && idEmployees.Contains(c.IdNhanVienSale.Value))).OrderByDescending(c => c.Id).Skip(query.Start).Take(query.Size).ToList();
                        total = resultFilter.Where(c => c.IdNhanVienSale == null || c.IdNhanVienSale == idEmployee || (c.IdNhanVienSale != null && idEmployees.Contains(c.IdNhanVienSale.Value))).Count();
                        break;
                    case "received":
                        data = resultFilter.Where(c => c.IdNhanVienSale == idEmployee || (c.IdNhanVienSale != null && c.IdUserGiaoViec == idUser) || (c.IdNhanVienSale != null && idEmployees.Contains(c.IdNhanVienSale.Value))).OrderByDescending(c => c.Id).Skip(query.Start).Take(query.Size).ToList();
                        total = resultFilter.Where(c => c.IdNhanVienSale == idEmployee || (c.IdNhanVienSale != null && c.IdUserGiaoViec == idUser) || (c.IdNhanVienSale != null && idEmployees.Contains(c.IdNhanVienSale.Value))).Count();
                        break;
                }
            }

            var result = new QueryResponse<CustomerDto>()
            {
                data = data,
                totalRow = total,
            };

            return result;
        }

        // Helper methods to apply additional conditions based on listType and role
        private IQueryable<TblDmcustomer> ApplyAllCondition(IQueryable<TblDmcustomer> query, string role, long idEmployee)
        {
            if (role == "employee")
            {
                query = query.Where(c => c.IdnhanVienSale == null || c.IdnhanVienSale == idEmployee);
            }
            return query;
        }

        private IQueryable<TblDmcustomer> ApplyAssignedCondition(IQueryable<TblDmcustomer> query, string role, long idUser, long idEmployee)
        {
            if (role == "admin")
            {
                query = query.Where(c => c.IdnhanVienSale != null && c.EnumGiaoNhan == 1);
            }
            else if (role == "manager")
            {
                query = query.Where(c => c.IdnhanVienSale != null && c.IduserGiaoViec == idUser && c.EnumGiaoNhan == 1);
            }
            return query;
        }

        private IQueryable<TblDmcustomer> ApplyDeliveredCondition(IQueryable<TblDmcustomer> query, long idEmployee)
        {
            query = query.Where(c => c.IdnhanVienSale == idEmployee && c.EnumGiaoNhan == 1);
            return query;
        }

        private IQueryable<TblDmcustomer> ApplyReceivedCondition(IQueryable<TblDmcustomer> query, string role, long idUser, long idEmployee)
        {
            if (role == "admin")
            {
                query = query.Where(c => c.IdnhanVienSale != null && c.EnumGiaoNhan == 2);
            }
            else if (role == "manager")
            {
                query = query.Where(c => c.EnumGiaoNhan == 2);
            }
            else if (role == "employee")
            {
                query = query.Where(c => c.IdnhanVienSale == idEmployee && c.EnumGiaoNhan == 2);
            }
            return query;
        }

        private IQueryable<TblDmcustomer> ApplyUndeliveredCondition(IQueryable<TblDmcustomer> query)
        {
            query = query.Where(c => c.EnumGiaoNhan == null || c.EnumGiaoNhan == 0 || c.EnumGiaoNhan == 3);
            return query;
        }

        public async Task AcceptCustomers(List<TblDmcustomer> data, AcceptCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.EnumGiaoNhan = 2;
                item.NgayNhan = DateTime.Now;
                item.NgayKetThucNhan = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task ChooseCustomers(List<TblDmcustomer> data, ChooseCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = req.IdNhanVienSale;
                item.EnumGiaoNhan = 2;
                item.NgayNhan = DateTime.Now;
                item.NgayKetThucNhan = null;
                item.ThongTinGiaoViec = "";
                item.IduserGiaoViec = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task Create(CreateCustomerRequest req)
        {
            var data = new TblDmcustomer()
            {
                IdquocGia = req.IdQuocGia != -1 ? req.IdQuocGia : null,
                Idcity = req.IdCity != -1 ? req.IdCity : null,
                Code = req.Code ?? "",
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
                AddressVI = req.AddressVI,
                AddressEN = req.AddressEN ?? "",
                TaxCode = req.TaxCode ?? "",
                Phone = req.Phone ?? "",
                Fax = req.Fax ?? "",
                Email = req.Email ?? "",
                Website = req.Website ?? "",
                Note = req.Note ?? "",
                IduserCreate = req.IdUserCreate,
                DateCreate = DateTime.Now,
                FlagActive = true,
                FlagDel = false,
                EnumGiaoNhan = 0,
                IdloaiDoanhNghiep = req.IdLoaiDoanhNghiep != -1 ? req.IdLoaiDoanhNghiep : null,
            };

            await _db.TblDmcustomers.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TblDmcustomer data, DeleteCustomerRequest req)
        {
            data.FlagActive = !req.FlagDel;
            data.FlagDel = req.FlagDel;
            data.LyDoXoa = req.LyDoXoa ?? "";
            data.DateDelete = req.FlagDel ? DateTime.Now : null;

            await _db.SaveChangesAsync();
        }

        public async Task DeliveryCustomers(List<TblDmcustomer> data, DeliveryCustomerRequest req)
        {
            var systemOps = await _db.TblSysOptions.FindAsync((long)1);

            foreach (var item in data)
            {
                item.IdnhanVienSale = req.IDNhanVienSale;
                item.IduserGiaoViec = req.IDUserGiaoViec;
                item.ThongTinGiaoViec = req.ThongTinGiaoViec;
                item.EnumGiaoNhan = 1;
                item.NgayGiao = DateTime.Now;
                item.NgayKetThucNhan = systemOps?.NgayNhanKhach != null ? DateTime.Now.AddDays(Convert.ToDouble(systemOps.NgayNhanKhach)) : DateTime.Now.AddDays(3);
            }

            await _db.SaveChangesAsync();
        }

        public async Task DenyCustomers(List<TblDmcustomer> data, DenyCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = null;
                item.EnumGiaoNhan = 3;
                item.LyDoTuChoi = req.LyDoTuChoi;
                item.NgayKetThucNhan = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task<TblDmcustomer?> GetById(long id)
        {
            TblDmcustomer? data = await _db.TblDmcustomers.Where(c => c.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<TblDmcustomer>?> GetCustomersByIdArray(long[] ids, long? IdNhanVien = null)
        {
            List<TblDmcustomer>? data;
            if (IdNhanVien != null)
            {
                data = await Task.FromResult(_db.TblDmcustomers.ToList().Where(x => ids.Contains(x.Id) && x.IdnhanVienSale == IdNhanVien).ToList());
            }
            else
            {
                data = await Task.FromResult(_db.TblDmcustomers.ToList().Where(x => ids.Contains(x.Id)).ToList());
            }
            return data;
        }

        public async Task<List<TblDmcustomer>?> GetCustomersData(int pageNumber, int pageSize, string permission, long idUser, long idEmployee, List<long> idEmployees)
        {
            List<TblDmcustomer>? data;

            if ((permission.Contains("1048576") || permission.Contains("7000")))
            {
                data = await _db.TblDmcustomers.Where(c => c.FlagDel != true).OrderBy(x => x.Id).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            }
            else if (permission.Contains("7080"))
            {
                var dataList = await _db.TblDmcustomers.AsNoTracking().Where(c => c.FlagDel != true).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
                data = dataList.Where(c => c.IdnhanVienSale == idEmployee || (idEmployees != null && idEmployees.Any(x1 => x1 == c.IdnhanVienSale)) || c.IdnhanVienSale == null).ToList();
            }
            else
            {
                data = await _db.TblDmcustomers.Where(c => c.FlagDel != true).Where(c => c.IdnhanVienSale == null || c.IdnhanVienSale == idEmployee).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            }

            return data;
        }

        public async Task<List<TblDmcustomer>?> GetCustomersReceivedData(int pageNumber, int pageSize, string permission, long idUser, long idEmployee, List<long> idEmployees)
        {
            List<TblDmcustomer>? data;

            if ((permission.Contains("1048576") || permission.Contains("7000")))
            {
                data = await _db.TblDmcustomers.Where(c => c.FlagDel != true && c.IdnhanVienSale != null && c.EnumGiaoNhan == 2).OrderBy(x => x.Id).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            }
            else if (permission.Contains("7080"))
            {
                var dataList = await _db.TblDmcustomers.AsNoTracking().Where(c => c.FlagDel != true && c.IdnhanVienSale != null && c.EnumGiaoNhan == 2).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
                data = dataList.Where(c => c.IdnhanVienSale == idEmployee || (idEmployees != null && idEmployees.Any(x1 => x1 == c.IdnhanVienSale))).ToList();
            }
            else
            {
                data = await _db.TblDmcustomers.Where(c => c.FlagDel != true && c.IdnhanVienSale == idEmployee && c.EnumGiaoNhan == 2).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            }

            return data;
        }

        public async Task<QueryResponse<CustomerDto>> GetData(CustomerQuery query, string permission, long idUser, long idEmployee, List<long> idEmployees)
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
            var data = await Query(query, role, idUser, idEmployee, idEmployees);
            return data;
        }

        public async Task<bool> IsExistCodeCustomer(string code)
        {
            if (code == "") return false;
            var data = await _db.TblDmcustomers.Where(x => x.Code == code).FirstOrDefaultAsync();
            if (data == null) return false;
            return true;
        }

        public async Task<bool> IsExistTaxCodeCustomer(string code)
        {
            if (code == "") return false;
            var data = await _db.TblDmcustomers.Where(x => x.TaxCode == code).FirstOrDefaultAsync();
            if (data == null) return false;
            return true;
        }

        public async Task Remove(TblDmcustomer data)
        {
            var customer = await _db.TblDmcustomers.Include(c => c.TblDmcontactLists).Include(c => c.TblCustomerListImExes)
                                                   .Include(c => c.TblCustomerTacNghieps).Include(c => c.TblDmcustomerDanhGia)
                                                   .Include(c => c.TblDmcustomerNghiepVus).Include(c => c.TblDmcustomerPhanLoaiKhs)
                                                   .Include(c => c.TblDmcustomerTuyenHangs).SingleOrDefaultAsync(c => c.Id == data.Id);

            if (customer != null)
            {
                _db.TblDmcontactLists.RemoveRange(customer.TblDmcontactLists);
                _db.TblCustomerTacNghieps.RemoveRange(customer.TblCustomerTacNghieps);
                _db.TblCustomerListImExes.RemoveRange(customer.TblCustomerListImExes);
                _db.TblDmcustomerDanhGia.RemoveRange(customer.TblDmcustomerDanhGia);
                _db.TblDmcustomerNghiepVus.RemoveRange(customer.TblDmcustomerNghiepVus);
                _db.TblDmcustomerPhanLoaiKhs.RemoveRange(customer.TblDmcustomerPhanLoaiKhs);
                _db.TblDmcustomerTuyenHangs.RemoveRange(customer.TblDmcustomerTuyenHangs);

                _db.TblDmcustomers.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UndeliveryCustomers(List<TblDmcustomer> data, UndeliveryCustomerRequest req)
        {
            foreach (var item in data)
            {
                item.IdnhanVienSale = null;
                item.IduserGiaoViec = null;
                item.ThongTinGiaoViec = "";
                item.EnumGiaoNhan = 0;
                item.NgayGiao = null;
                item.NgayNhan = null;
                item.NgayKetThucNhan = null;
                item.LyDoTuChoi = null;
            }

            await _db.SaveChangesAsync();
        }

        public async Task Update(TblDmcustomer data, UpdateCustomerRequest req)
        {
            data.IdloaiDoanhNghiep = req.IdLoaiDoanhNghiep != null && req.IdLoaiDoanhNghiep != -1 ? req.IdLoaiDoanhNghiep : data.IdloaiDoanhNghiep;
            data.IdquocGia = req.IdQuocGia != null && req.IdQuocGia != -1 ? req.IdQuocGia : data.IdquocGia;
            data.Idcity = req.IdCity != null && req.IdCity != -1 ? req.IdCity : data.Idcity;
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;
            data.AddressVI = req.AddressVI ?? data.AddressVI;
            data.AddressEN = req.AddressEN ?? data.AddressEN;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;

            await _db.SaveChangesAsync();
        }
    }
}
