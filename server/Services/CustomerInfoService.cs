using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Services
{
    public class CustomerInfoService : ICustomerInfoService
    {
        private readonly VslDbContext _db;

        public CustomerInfoService(VslDbContext db)
        {
            _db = db;
        }

        public async Task CreateCustomerClassify(CreateCustomerClassifyRequest req)
        {
            var data = new TblDmcustomerPhanLoaiKh()
            {
                Iddmcustomer = req.IdCustomer,
                IddmphanLoaiKhachHang = req.IdPhanLoaiKhachHang != null && req.IdPhanLoaiKhachHang != -1 ? req.IdPhanLoaiKhachHang : null,
            };

            await _db.TblDmcustomerPhanLoaiKhs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerContact(CreateCustomerContactRequest req)
        {
            var data = new TblDmcontactList()
            {
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
                AddressVI = req.AddressVI ?? "",
                AddressEN = req.AddressEN ?? "",
                EnumGioiTinh = (req.EnumGioiTinh != null && req.EnumGioiTinh != -1) ? req.EnumGioiTinh : null,
                HandPhone = req.HandPhone ?? "",
                HomePhone = req.HomePhone ?? "",
                Email = req.Email ?? "",
                Note = req.Note ?? "",
                Idcustomer = req.IdCustomer,
                BankAccountNumber = req.BankAccountNumber ?? "",
                BankBranchName = req.BankBranchName ?? "",
                BankAddress = req.BankAddress ?? "",
                Chat = req.Chat ?? "",
                FlagDaiDien = req.FlagDaiDien ?? false,
                FlagActive = true,
                FlagFavorite = false,
                ChucVu = req.ChucVu ?? "",
            };

            await _db.TblDmcontactLists.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerEvaluate(CreateCustomerEvaluateRequest req)
        {
            var data = new TblDmcustomerDanhGium()
            {
                Iddmcustomer = req.IdCustomer,
                IddmcustomerType = req.IdCustomerType != null && req.IdCustomerType != -1 ? req.IdCustomerType : null,
                IduserCreate = req.IdUserCreate,
                GhiChu = req.GhiChu ?? "",
                DateCreate = DateTime.Now,
            };

            await _db.TblDmcustomerDanhGia.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerImEx(CreateCustomerImExRequest req)
        {
            var data = new TblCustomerListImEx()
            {
                Date = (req.Date != null && req.Date != "") ? DateTime.Parse(req.Date) : null,
                Type = req.Type ?? "",
                Vessel = req.Vessel ?? "",
                Term = req.Term ?? "",
                Code = req.Code ?? "",
                Commd = req.Commd ?? "",
                Vol = req.Vol ?? "",
                Unt = req.Unt ?? "",
                IduserCreate = req.IdUserCreate,
                IdfromPort = req.IdFromPort,
                IdtoPort = req.IdToPort,
                IdfromCountry = req.IdFromCountry,
                IdtoCountry = req.IdToCountry,
                Iddmcustomer = req.IdCustomer,
                CreateDate = DateTime.Now,
            };

            await _db.TblCustomerListImExes.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerMajor(CreateCustomerMajorRequest req)
        {
            var data = new TblDmcustomerNghiepVu()
            {
                Iddmcustomer = req.IdCustomer,
                IddmnghiepVu = req.IdNghiepVu != null && req.IdNghiepVu != -1 ? req.IdNghiepVu : null,
            };

            await _db.TblDmcustomerNghiepVus.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerOperational(CreateCustomerOperationalRequest req, TblDmcustomer? customer)
        {
            var operational = await _db.TblDmloaiTacNghieps.Where(x => x.Id == req.IdLoaiTacNghiep).Select(x => new { x.R, x.B, x.G }).FirstOrDefaultAsync();

            var data = new TblCustomerTacNghiep()
            {
                IdloaiTacNghiep = req.IdLoaiTacNghiep,
                NoiDung = req.NoiDung ?? "",
                IduserCreate = req.IdUserCreate,
                Iddmcustomer = req.IdCustomer,
                ThoiGianThucHien = (req.ThoiGianThucHien != null && req.ThoiGianThucHien != "") ? DateTime.Parse(req.ThoiGianThucHien) : DateTime.Now,
                IdnguoiLienHe = (req.IdNguoiLienHe != null && req.IdNguoiLienHe != -1) ? req.IdNguoiLienHe : null,
                KhachHangPhanHoi = req.KhachHangPhanHoi ?? "",
                NgayPhanHoi = (req.NgayPhanHoi != null && req.NgayPhanHoi != "") ? DateTime.Parse(req.NgayPhanHoi) : null,
                DateCreate = DateTime.Now,
            };

            if (operational != null && customer != null)
            {
                customer.MauTacNghiepCuoi = "rgb(" + operational.R + "," + operational.G + "," + operational.B + ")";
            }

            await _db.TblCustomerTacNghieps.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerRoute(CreateCustomerRouteRequest req)
        {
            var data = new TblDmcustomerTuyenHang()
            {
                IdquocGiaDi = req.IdQuocGiaDi,
                IdquocGiaDen = req.IdQuocGiaDen,
                IdcangDi = req.IdCangDi,
                IdcangDen = req.IdCangDen,
                Iddmcustomer = req.IdCustomer,
                IddmloaiHinhVanChuyen = req.IdLoaiHinhVanChuyen,
            };

            await _db.TblDmcustomerTuyenHangs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerClassify(TblDmcustomerPhanLoaiKh data)
        {
            _db.TblDmcustomerPhanLoaiKhs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerContact(TblDmcontactList data)
        {
            _db.TblDmcontactLists.Remove(data);
            await _db.SaveChangesAsync(); ;
        }

        public async Task DeleteCustomerEvaluate(TblDmcustomerDanhGium data)
        {
            _db.TblDmcustomerDanhGia.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerImEx(TblCustomerListImEx data)
        {
            _db.TblCustomerListImExes.Remove(data);
            await _db.SaveChangesAsync();

        }

        public async Task DeleteCustomerMajor(TblDmcustomerNghiepVu data)
        {
            _db.TblDmcustomerNghiepVus.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerOperational(TblCustomerTacNghiep data)
        {
            _db.TblCustomerTacNghieps.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerRoute(TblDmcustomerTuyenHang data)
        {
            _db.TblDmcustomerTuyenHangs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<CustomerContactDto>?> GetAllCustomerContacts(long idCustomer = 0)
        {
            List<CustomerContactDto>? data = await _db.TblDmcontactLists.Select(x => new CustomerContactDto()
            {
                Id = x.Id,
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
                AddressVI = x.AddressVI ?? "",
                AddressEN = x.AddressEN ?? "",
                EnumGioiTinh = x.EnumGioiTinh ?? 0,
                HandPhone = x.HandPhone ?? "",
                HomePhone = x.HomePhone ?? "",
                Email = x.Email ?? "",
                Note = x.Note ?? "",
                IdCustomer = x.Idcustomer,
                FlagFavorite = x.FlagFavorite ?? false,
                BankAccountNumber = x.BankAccountNumber ?? "",
                BankBranchName = x.BankBranchName ?? "",
                BankAddress = x.BankAddress ?? "",
                Chat = x.Chat ?? "",
                FlagActive = x.FlagActive ?? false,
                FlagDaiDien = x.FlagDaiDien ?? false,
                ChucVu = x.ChucVu ?? "",
            }).Where(x => x.IdCustomer == idCustomer).ToListAsync();

            return data;
        }

        public async Task<List<CustomerClassifyDto>?> GetCustomerClassifies(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerClassifyDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomerPhanLoaiKhs.Select(x => new CustomerClassifyDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdPhanLoaiKhachHang = x.IddmphanLoaiKhachHang,
                    PhanLoaiKhachHang = _db.TblDmphanLoaiKhachHangs.Where(y => y.Id == x.IddmphanLoaiKhachHang).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcustomerPhanLoaiKhs.Select(x => new CustomerClassifyDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdPhanLoaiKhachHang = x.IddmphanLoaiKhachHang,
                    PhanLoaiKhachHang = _db.TblDmphanLoaiKhachHangs.Where(y => y.Id == x.IddmphanLoaiKhachHang).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).Where(x => x.PhanLoaiKhachHang != null && x.PhanLoaiKhachHang.Contains(Search))
                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmcustomerPhanLoaiKh?> GetCustomerClassifyById(long id)
        {
            TblDmcustomerPhanLoaiKh? data = await _db.TblDmcustomerPhanLoaiKhs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<TblDmcontactList?> GetCustomerContactById(long id)
        {
            TblDmcontactList? data = await _db.TblDmcontactLists.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerContactDto>?> GetCustomerContacts(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerContactDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcontactLists.Select(x => new CustomerContactDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    EnumGioiTinh = x.EnumGioiTinh ?? -1,
                    HandPhone = x.HandPhone ?? "",
                    HomePhone = x.HomePhone ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    IdCustomer = x.Idcustomer,
                    FlagFavorite = x.FlagFavorite ?? false,
                    BankAccountNumber = x.BankAccountNumber ?? "",
                    BankBranchName = x.BankBranchName ?? "",
                    BankAddress = x.BankAddress ?? "",
                    Chat = x.Chat ?? "",
                    FlagActive = x.FlagActive ?? false,
                    FlagDaiDien = x.FlagDaiDien ?? false,
                    ChucVu = x.ChucVu ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcontactLists.Select(x => new CustomerContactDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    EnumGioiTinh = x.EnumGioiTinh ?? -1,
                    HandPhone = x.HandPhone ?? "",
                    HomePhone = x.HomePhone ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    IdCustomer = x.Idcustomer,
                    FlagFavorite = x.FlagFavorite ?? false,
                    BankAccountNumber = x.BankAccountNumber ?? "",
                    BankBranchName = x.BankBranchName ?? "",
                    BankAddress = x.BankAddress ?? "",
                    Chat = x.Chat ?? "",
                    FlagActive = x.FlagActive ?? false,
                    FlagDaiDien = x.FlagDaiDien ?? false,
                    ChucVu = x.ChucVu ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                            (x.Email != null && x.Email.Contains(Search)) || (x.Note != null && x.Note.Contains(Search)) ||
                            (x.Chat != null && x.Chat.Contains(Search)) || (x.ChucVu != null && x.ChucVu.Contains(Search))
                ).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmcustomerDanhGium?> GetCustomerEvaluateById(long id)
        {
            TblDmcustomerDanhGium? data = await _db.TblDmcustomerDanhGia.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerEvaluateDto>?> GetCustomerEvaluates(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerEvaluateDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomerDanhGia.Select(x => new CustomerEvaluateDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdCustomerType = x.IddmcustomerType,
                    IdUserCreate = x.IduserCreate,
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    GhiChu = x.GhiChu ?? "",
                    LoaiKhachHang = _db.TblDmcustomerTypes.Where(y => y.Id == x.IddmcustomerType).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcustomerDanhGia.Select(x => new CustomerEvaluateDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdCustomerType = x.IddmcustomerType,
                    IdUserCreate = x.IduserCreate,
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    GhiChu = x.GhiChu ?? "",
                    LoaiKhachHang = _db.TblDmcustomerTypes.Where(y => y.Id == x.IddmcustomerType).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.LoaiKhachHang != null && x.LoaiKhachHang.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) || (x.GhiChu != null && x.GhiChu.Contains(Search)))
                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblCustomerListImEx?> GetCustomerImExById(long id)
        {
            TblCustomerListImEx? data = await _db.TblCustomerListImExes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerListImExDto>?> GetCustomerListImEx(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerListImExDto>? data;

            if (Search == "")
            {
                data = await _db.TblCustomerListImExes.Select(x => new CustomerListImExDto()
                {
                    Id = x.Id,
                    Date = x.Date != null ? string.Format("{0:yyyy-MM-dd}", x.Date) : "",
                    Type = x.Type ?? "",
                    Vessel = x.Vessel ?? "",
                    Term = x.Term ?? "",
                    Code = x.Code ?? "",
                    Commd = x.Commd ?? "",
                    Vol = x.Vol ?? "",
                    Unt = x.Unt ?? "",
                    IdCustomer = x.Iddmcustomer,
                    CreateDate = x.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", x.CreateDate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdFromPort = x.IdfromPort,
                    IdToPort = x.IdtoPort,
                    IdFromCountry = x.IdfromCountry,
                    IdToCountry = x.IdtoCountry,
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdfromPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdtoPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdfromCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdtoCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblCustomerListImExes.Select(x => new CustomerListImExDto()
                {
                    Id = x.Id,
                    Date = x.Date != null ? string.Format("{0:yyyy-MM-dd}", x.Date) : "",
                    Type = x.Type ?? "",
                    Vessel = x.Vessel ?? "",
                    Term = x.Term ?? "",
                    Code = x.Code ?? "",
                    Commd = x.Commd ?? "",
                    Vol = x.Vol ?? "",
                    Unt = x.Unt ?? "",
                    IdCustomer = x.Iddmcustomer,
                    CreateDate = x.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", x.CreateDate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdFromPort = x.IdfromPort,
                    IdToPort = x.IdtoPort,
                    IdFromCountry = x.IdfromCountry,
                    IdToCountry = x.IdtoCountry,
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdfromPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdtoPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdfromCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdtoCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.Type != null && x.Type.Contains(Search)) || (x.Vessel != null && x.Vessel.Contains(Search)) || (x.Term != null && x.Term.Contains(Search)) || (x.Code != null && x.Code.Contains(Search)) ||
                            (x.Commd != null && x.Commd.Contains(Search)) || (x.Vol != null && x.Vol.Contains(Search)) || (x.Unt != null && x.Unt.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) ||
                            (x.CangDi != null && x.CangDi.Contains(Search)) || (x.CangDen != null && x.CangDen.Contains(Search)) || (x.QuocGiaDi != null && x.QuocGiaDi.Contains(Search)) || (x.QuocGiaDen != null && x.QuocGiaDen.Contains(Search))
                ).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmcustomerNghiepVu?> GetCustomerMajorById(long id)
        {
            TblDmcustomerNghiepVu? data = await _db.TblDmcustomerNghiepVus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerMajorDto>?> GetCustomerMajors(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerMajorDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomerNghiepVus.Select(x => new CustomerMajorDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdNghiepVu = x.IddmnghiepVu,
                    NghiepVu = _db.TblDmnghiepVus.Where(y => y.Id == x.IddmnghiepVu).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcustomerNghiepVus.Select(x => new CustomerMajorDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdNghiepVu = x.IddmnghiepVu,
                    NghiepVu = _db.TblDmnghiepVus.Where(y => y.Id == x.IddmnghiepVu).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).Where(x => x.NghiepVu != null && x.NghiepVu.Contains(Search))
                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblCustomerTacNghiep?> GetCustomerOperationalById(long id)
        {
            TblCustomerTacNghiep? data = await _db.TblCustomerTacNghieps.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerOperationalDto>?> GetCustomerOperationals(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerOperationalDto>? data;

            if (Search == "")
            {
                data = await _db.TblCustomerTacNghieps.Select(x => new CustomerOperationalDto()
                {
                    Id = x.Id,
                    IdLoaiTacNghiep = x.IdloaiTacNghiep,
                    NoiDung = x.NoiDung ?? "",
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdCustomer = x.Iddmcustomer,
                    IdNguoiLienHe = x.IdnguoiLienHe,
                    KhachHangPhanHoi = x.KhachHangPhanHoi,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    NgayPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : "",
                    LoaiTacNghiep = _db.TblDmloaiTacNghieps.Where(y => y.Id == x.IdloaiTacNghiep).Select(y => y.Name).FirstOrDefault() ?? "",
                    NguoiLienHe = _db.TblDmcontactLists.Where(y => y.Id == x.IdnguoiLienHe).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblCustomerTacNghieps.Select(x => new CustomerOperationalDto()
                {
                    Id = x.Id,
                    IdLoaiTacNghiep = x.IdloaiTacNghiep,
                    NoiDung = x.NoiDung ?? "",
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdCustomer = x.Iddmcustomer,
                    IdNguoiLienHe = x.IdnguoiLienHe,
                    KhachHangPhanHoi = x.KhachHangPhanHoi,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    NgayPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : "",
                    LoaiTacNghiep = _db.TblDmloaiTacNghieps.Where(y => y.Id == x.IdloaiTacNghiep).Select(y => y.Name).FirstOrDefault() ?? "",
                    NguoiLienHe = _db.TblDmcontactLists.Where(y => y.Id == x.IdnguoiLienHe).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.LoaiTacNghiep != null && x.LoaiTacNghiep.Contains(Search)) || (x.NoiDung != null && x.NoiDung.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) ||
                            (x.NguoiLienHe != null && x.NguoiLienHe.Contains(Search)) || (x.KhachHangPhanHoi != null && x.KhachHangPhanHoi.Contains(Search))
                ).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmcustomerTuyenHang?> GetCustomerRouteById(long id)
        {
            TblDmcustomerTuyenHang? data = await _db.TblDmcustomerTuyenHangs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerRouteDto>?> GetCustomerRoutes(int Start = 0, int Size = 10, string Search = "", long idCustomer = 0)
        {
            List<CustomerRouteDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomerTuyenHangs.Select(x => new CustomerRouteDto()
                {
                    Id = x.Id,
                    IdQuocGiaDi = x.IdquocGiaDi,
                    IdQuocGiaDen = x.IdquocGiaDen,
                    IdCangDi = x.IdcangDi,
                    IdCangDen = x.IdcangDen,
                    IdCustomer = x.Iddmcustomer,
                    IdLoaiHinhVanChuyen = x.IddmloaiHinhVanChuyen,
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdcangDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdcangDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    LoaiHinhVanChuyen = _db.TblDmloaiHinhVanChuyens.Where(y => y.Id == x.IddmloaiHinhVanChuyen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcustomerTuyenHangs.Select(x => new CustomerRouteDto()
                {
                    Id = x.Id,
                    IdQuocGiaDi = x.IdquocGiaDi,
                    IdQuocGiaDen = x.IdquocGiaDen,
                    IdCangDi = x.IdcangDi,
                    IdCangDen = x.IdcangDen,
                    IdCustomer = x.Iddmcustomer,
                    IdLoaiHinhVanChuyen = x.IddmloaiHinhVanChuyen,
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdcangDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdcangDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    LoaiHinhVanChuyen = _db.TblDmloaiHinhVanChuyens.Where(y => y.Id == x.IddmloaiHinhVanChuyen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.QuocGiaDi != null && x.QuocGiaDi.Contains(Search)) || (x.QuocGiaDen != null && x.QuocGiaDen.Contains(Search)) ||
                            (x.CangDi != null && x.CangDi.Contains(Search)) || (x.CangDen != null && x.CangDen.Contains(Search)) ||
                            (x.LoaiHinhVanChuyen != null && x.LoaiHinhVanChuyen.Contains(Search))
                ).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<CustomerDto?> GetCustomersById(long id)
        {
            CustomerDto? data = await _db.TblDmcustomers.Select(c => new CustomerDto()
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
            }).Where(c => c.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<int> GetTotalCustomerClassifies(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcustomerPhanLoaiKhs.Select(x => new CustomerClassifyDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdPhanLoaiKhachHang = x.IddmphanLoaiKhachHang,
                    PhanLoaiKhachHang = _db.TblDmphanLoaiKhachHangs.Where(y => y.Id == x.IddmphanLoaiKhachHang).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblDmcustomerPhanLoaiKhs.Select(x => new CustomerClassifyDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdPhanLoaiKhachHang = x.IddmphanLoaiKhachHang,
                    PhanLoaiKhachHang = _db.TblDmphanLoaiKhachHangs.Where(y => y.Id == x.IddmphanLoaiKhachHang).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).Where(x => x.PhanLoaiKhachHang != null && x.PhanLoaiKhachHang.Contains(Search))
                .CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerContacts(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcontactLists.Select(x => new CustomerContactDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    EnumGioiTinh = x.EnumGioiTinh ?? -1,
                    HandPhone = x.HandPhone ?? "",
                    HomePhone = x.HomePhone ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    IdCustomer = x.Idcustomer,
                    FlagFavorite = x.FlagFavorite ?? false,
                    BankAccountNumber = x.BankAccountNumber ?? "",
                    BankBranchName = x.BankBranchName ?? "",
                    BankAddress = x.BankAddress ?? "",
                    Chat = x.Chat ?? "",
                    FlagActive = x.FlagActive ?? false,
                    FlagDaiDien = x.FlagDaiDien ?? false,
                    ChucVu = x.ChucVu ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblDmcontactLists.Select(x => new CustomerContactDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    EnumGioiTinh = x.EnumGioiTinh ?? -1,
                    HandPhone = x.HandPhone ?? "",
                    HomePhone = x.HomePhone ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    IdCustomer = x.Idcustomer,
                    FlagFavorite = x.FlagFavorite ?? false,
                    BankAccountNumber = x.BankAccountNumber ?? "",
                    BankBranchName = x.BankBranchName ?? "",
                    BankAddress = x.BankAddress ?? "",
                    Chat = x.Chat ?? "",
                    FlagActive = x.FlagActive ?? false,
                    FlagDaiDien = x.FlagDaiDien ?? false,
                    ChucVu = x.ChucVu ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                            (x.Email != null && x.Email.Contains(Search)) || (x.Note != null && x.Note.Contains(Search)) ||
                            (x.Chat != null && x.Chat.Contains(Search)) || (x.ChucVu != null && x.ChucVu.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerEvaluates(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcustomerDanhGia.Select(x => new CustomerEvaluateDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdCustomerType = x.IddmcustomerType ?? -1,
                    IdUserCreate = x.IduserCreate,
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    GhiChu = x.GhiChu ?? "",
                    LoaiKhachHang = _db.TblDmcustomerTypes.Where(y => y.Id == x.IddmcustomerType).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblDmcustomerDanhGia.Select(x => new CustomerEvaluateDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdCustomerType = x.IddmcustomerType,
                    IdUserCreate = x.IduserCreate,
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    GhiChu = x.GhiChu ?? "",
                    LoaiKhachHang = _db.TblDmcustomerTypes.Where(y => y.Id == x.IddmcustomerType).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.LoaiKhachHang != null && x.LoaiKhachHang.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) || (x.GhiChu != null && x.GhiChu.Contains(Search)))
                .CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerListImEx(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblCustomerListImExes.Select(x => new CustomerListImExDto()
                {
                    Id = x.Id,
                    Date = x.Date != null ? string.Format("{0:yyyy-MM-dd}", x.Date) : "",
                    Type = x.Type ?? "",
                    Vessel = x.Vessel ?? "",
                    Term = x.Term ?? "",
                    Code = x.Code ?? "",
                    Commd = x.Commd ?? "",
                    Vol = x.Vol ?? "",
                    Unt = x.Unt ?? "",
                    IdCustomer = x.Iddmcustomer,
                    CreateDate = x.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", x.CreateDate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdFromPort = x.IdfromPort,
                    IdToPort = x.IdtoPort,
                    IdFromCountry = x.IdfromCountry,
                    IdToCountry = x.IdtoCountry,
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdfromPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdtoPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdfromCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdtoCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblCustomerListImExes.Select(x => new CustomerListImExDto()
                {
                    Id = x.Id,
                    Date = x.Date != null ? string.Format("{0:yyyy-MM-dd}", x.Date) : "",
                    Type = x.Type ?? "",
                    Vessel = x.Vessel ?? "",
                    Term = x.Term ?? "",
                    Code = x.Code ?? "",
                    Commd = x.Commd ?? "",
                    Vol = x.Vol ?? "",
                    Unt = x.Unt ?? "",
                    IdCustomer = x.Iddmcustomer,
                    CreateDate = x.CreateDate != null ? string.Format("{0:yyyy-MM-dd}", x.CreateDate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdFromPort = x.IdfromPort,
                    IdToPort = x.IdtoPort,
                    IdFromCountry = x.IdfromCountry,
                    IdToCountry = x.IdtoCountry,
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdfromPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdtoPort).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdfromCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdtoCountry).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.Type != null && x.Type.Contains(Search)) || (x.Vessel != null && x.Vessel.Contains(Search)) || (x.Term != null && x.Term.Contains(Search)) || (x.Code != null && x.Code.Contains(Search)) ||
                            (x.Commd != null && x.Commd.Contains(Search)) || (x.Vol != null && x.Vol.Contains(Search)) || (x.Unt != null && x.Unt.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) ||
                            (x.CangDi != null && x.CangDi.Contains(Search)) || (x.CangDen != null && x.CangDen.Contains(Search)) || (x.QuocGiaDi != null && x.QuocGiaDi.Contains(Search)) || (x.QuocGiaDen != null && x.QuocGiaDen.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerMajors(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcustomerNghiepVus.Select(x => new CustomerMajorDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdNghiepVu = x.IddmnghiepVu,
                    NghiepVu = _db.TblDmnghiepVus.Where(y => y.Id == x.IddmnghiepVu).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblDmcustomerNghiepVus.Select(x => new CustomerMajorDto()
                {
                    Id = x.Id,
                    IdCustomer = x.Iddmcustomer,
                    IdNghiepVu = x.IddmnghiepVu,
                    NghiepVu = _db.TblDmnghiepVus.Where(y => y.Id == x.IddmnghiepVu).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).Where(x => x.NghiepVu != null && x.NghiepVu.Contains(Search))
                .CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerOperationals(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblCustomerTacNghieps.Select(x => new CustomerOperationalDto()
                {
                    Id = x.Id,
                    IdLoaiTacNghiep = x.IdloaiTacNghiep,
                    NoiDung = x.NoiDung ?? "",
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdCustomer = x.Iddmcustomer,
                    IdNguoiLienHe = x.IdnguoiLienHe,
                    KhachHangPhanHoi = x.KhachHangPhanHoi,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    NgayPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : "",
                    LoaiTacNghiep = _db.TblDmloaiTacNghieps.Where(y => y.Id == x.IdloaiTacNghiep).Select(y => y.Name).FirstOrDefault() ?? "",
                    NguoiLienHe = _db.TblDmcontactLists.Where(y => y.Id == x.IdnguoiLienHe).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblCustomerTacNghieps.Select(x => new CustomerOperationalDto()
                {
                    Id = x.Id,
                    IdLoaiTacNghiep = x.IdloaiTacNghiep,
                    NoiDung = x.NoiDung ?? "",
                    DateCreate = x.DateCreate != null ? string.Format("{0:yyyy-MM-dd}", x.DateCreate) : "",
                    IdUserCreate = x.IduserCreate,
                    IdCustomer = x.Iddmcustomer,
                    IdNguoiLienHe = x.IdnguoiLienHe,
                    KhachHangPhanHoi = x.KhachHangPhanHoi,
                    ThoiGianThucHien = x.ThoiGianThucHien != null ? string.Format("{0:yyyy-MM-dd}", x.ThoiGianThucHien) : "",
                    NgayPhanHoi = x.NgayPhanHoi != null ? string.Format("{0:yyyy-MM-dd}", x.NgayPhanHoi) : "",
                    LoaiTacNghiep = _db.TblDmloaiTacNghieps.Where(y => y.Id == x.IdloaiTacNghiep).Select(y => y.Name).FirstOrDefault() ?? "",
                    NguoiLienHe = _db.TblDmcontactLists.Where(y => y.Id == x.IdnguoiLienHe).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    NguoiTao = x.IduserCreate == 1 ? "admin" : _db.TblSysUsers.Where(y => y.Id == x.IduserCreate).Join(_db.TblNhanSus, z => z.IdnhanVien, y => y.Id, (z, y) => new { HoTen = y.HoTenVI ?? "" }).Select(y => y.HoTen).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.LoaiTacNghiep != null && x.LoaiTacNghiep.Contains(Search)) || (x.NoiDung != null && x.NoiDung.Contains(Search)) || (x.NguoiTao != null && x.NguoiTao.Contains(Search)) ||
                            (x.NguoiLienHe != null && x.NguoiLienHe.Contains(Search)) || (x.KhachHangPhanHoi != null && x.KhachHangPhanHoi.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerRoutes(string Search = "", long idCustomer = 0)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcustomerTuyenHangs.Select(x => new CustomerRouteDto()
                {
                    Id = x.Id,
                    IdQuocGiaDi = x.IdquocGiaDi,
                    IdQuocGiaDen = x.IdquocGiaDen,
                    IdCangDi = x.IdcangDi,
                    IdCangDen = x.IdcangDen,
                    IdCustomer = x.Iddmcustomer,
                    IdLoaiHinhVanChuyen = x.IddmloaiHinhVanChuyen,
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdcangDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdcangDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    LoaiHinhVanChuyen = _db.TblDmloaiHinhVanChuyens.Where(y => y.Id == x.IddmloaiHinhVanChuyen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer).CountAsync();
            }
            else
            {
                total = await _db.TblDmcustomerTuyenHangs.Select(x => new CustomerRouteDto()
                {
                    Id = x.Id,
                    IdQuocGiaDi = x.IdquocGiaDi,
                    IdQuocGiaDen = x.IdquocGiaDen,
                    IdCangDi = x.IdcangDi,
                    IdCangDen = x.IdcangDen,
                    IdCustomer = x.Iddmcustomer,
                    IdLoaiHinhVanChuyen = x.IddmloaiHinhVanChuyen,
                    QuocGiaDi = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    QuocGiaDen = _db.TblDmcountries.Where(y => y.Id == x.IdquocGiaDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDi = _db.TblDmports.Where(y => y.Id == x.IdcangDi).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    CangDen = _db.TblDmports.Where(y => y.Id == x.IdcangDen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                    LoaiHinhVanChuyen = _db.TblDmloaiHinhVanChuyens.Where(y => y.Id == x.IddmloaiHinhVanChuyen).Select(y => y.NameVI).FirstOrDefault() ?? "",
                }).Where(x => x.IdCustomer == idCustomer)
                .Where(x => (x.QuocGiaDi != null && x.QuocGiaDi.Contains(Search)) || (x.QuocGiaDen != null && x.QuocGiaDen.Contains(Search)) ||
                            (x.CangDi != null && x.CangDi.Contains(Search)) || (x.CangDen != null && x.CangDen.Contains(Search)) ||
                            (x.LoaiHinhVanChuyen != null && x.LoaiHinhVanChuyen.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task UpdateCustomerClassify(TblDmcustomerPhanLoaiKh data, UpdateCustomerClassifyRequest req)
        {
            data.IddmphanLoaiKhachHang = req.IdPhanLoaiKhachHang != null && req.IdPhanLoaiKhachHang != -1 ? req.IdPhanLoaiKhachHang : data.IddmphanLoaiKhachHang;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerContact(TblDmcontactList data, UpdateCustomerContactRequest req)
        {
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;
            data.AddressVI = req.AddressVI ?? data.AddressVI;
            data.AddressEN = req.AddressEN ?? data.AddressEN;
            data.EnumGioiTinh = req.EnumGioiTinh != null && req.EnumGioiTinh != -1 ? req.EnumGioiTinh : data.EnumGioiTinh;
            data.HandPhone = req.HandPhone ?? data.HandPhone;
            data.HomePhone = req.HomePhone ?? data.HomePhone;
            data.Email = req.Email ?? data.Email;
            data.Note = req.Note ?? data.Note;
            data.BankAccountNumber = req.BankAccountNumber ?? data.BankAccountNumber;
            data.BankBranchName = req.BankBranchName ?? data.BankBranchName;
            data.BankAddress = req.BankAddress ?? data.BankAddress;
            data.Chat = req.Chat ?? data.Chat;
            data.FlagDaiDien = req.FlagDaiDien ?? data.FlagDaiDien;
            data.ChucVu = req.ChucVu ?? data.ChucVu;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerEvaluate(TblDmcustomerDanhGium data, UpdateCustomerEvaluateRequest req)
        {
            data.IddmcustomerType = req.IdCustomerType != null && req.IdCustomerType != -1 ? req.IdCustomerType : data.IddmcustomerType;
            data.GhiChu = req.GhiChu;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerImEx(TblCustomerListImEx data, UpdateCustomerImExRequest req)
        {
            data.Date = (req.Date != null && req.Date != "") ? DateTime.Parse(req.Date) : data.Date;
            data.Type = req.Type ?? data.Type;
            data.Vessel = req.Vessel ?? data.Vessel;
            data.Term = req.Term ?? data.Term;
            data.Code = req.Code ?? data.Code;
            data.Commd = req.Commd ?? data.Commd;
            data.Vol = req.Vol ?? data.Vol;
            data.Unt = req.Unt ?? data.Unt;
            data.IdfromPort = req.IdFromPort ?? data.IdfromPort;
            data.IdtoPort = req.IdToPort ?? data.IdtoPort;
            data.IdfromCountry = req.IdFromCountry ?? data.IdfromCountry;
            data.IdtoCountry = req.IdToCountry ?? data.IdtoCountry;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerMajor(TblDmcustomerNghiepVu data, UpdateCustomerMajorRequest req)
        {
            data.IddmnghiepVu = req.IdNghiepVu != null && req.IdNghiepVu != -1 ? req.IdNghiepVu : data.IddmnghiepVu;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerOperational(TblCustomerTacNghiep data, UpdateCustomerOperationalRequest req)
        {
            data.IdloaiTacNghiep = req.IdLoaiTacNghiep ?? data.IdloaiTacNghiep;
            data.NoiDung = req.NoiDung ?? data.NoiDung;
            data.ThoiGianThucHien = (req.ThoiGianThucHien != null && req.ThoiGianThucHien != "") ? DateTime.Parse(req.ThoiGianThucHien) : data.ThoiGianThucHien;
            data.IdnguoiLienHe = req.IdNguoiLienHe ?? data.IdnguoiLienHe;
            data.KhachHangPhanHoi = req.KhachHangPhanHoi ?? data.KhachHangPhanHoi;
            data.NgayPhanHoi = (req.NgayPhanHoi != null && req.NgayPhanHoi != "") ? DateTime.Parse(req.NgayPhanHoi) : data.NgayPhanHoi;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerRoute(TblDmcustomerTuyenHang data, UpdateCustomerRouteRequest req)
        {
            data.IdquocGiaDi = (req.IdQuocGiaDi != null && req.IdQuocGiaDi != -1 && req.IdQuocGiaDi != data.IdquocGiaDi) ? req.IdQuocGiaDi : data.IdquocGiaDi;
            data.IdquocGiaDen = (req.IdQuocGiaDen != null && req.IdQuocGiaDen != -1 && req.IdQuocGiaDen != data.IdquocGiaDen) ? req.IdQuocGiaDen : data.IdquocGiaDen;
            data.IdcangDi = (req.IdCangDi != null && req.IdCangDi != -1 && req.IdCangDi != data.IdcangDi) ? req.IdCangDi : data.IdcangDi;
            data.IdcangDen = (req.IdCangDen != null && req.IdCangDen != -1 && req.IdCangDen != data.IdcangDen) ? req.IdCangDen : data.IdcangDen;
            data.IddmloaiHinhVanChuyen = (req.IdLoaiHinhVanChuyen != null && req.IdLoaiHinhVanChuyen != -1 && req.IdLoaiHinhVanChuyen != data.IddmloaiHinhVanChuyen) ? req.IdLoaiHinhVanChuyen : data.IddmloaiHinhVanChuyen;

            await _db.SaveChangesAsync();
        }
    }
}
