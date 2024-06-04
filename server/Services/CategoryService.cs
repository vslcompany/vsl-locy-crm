using Microsoft.EntityFrameworkCore;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly VslDbContext _db;

        public CategoryService(VslDbContext db)
        {
            _db = db;
        }

        public async Task CreateBusiness(CreateBusinessRequest req)
        {
            var data = new TblDmloaiDoanhNghiep()
            {
                Code = req.Code ?? "",
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
            };

            await _db.TblDmloaiDoanhNghieps.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCity(CreateCityRequest req)
        {
            var data = new TblDmcity()
            {
                Code = req.Code ?? "",
                IdquocGia = req.IdQuocGia,
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
            };

            await _db.TblDmcities.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCountry(CreateCountryRequest req)
        {
            var data = new TblDmcountry()
            {
                Code = req.Code ?? "",
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
            };

            await _db.TblDmcountries.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCustomerType(CreateCustomerTypeRequest req)
        {
            var data = new TblDmcustomerType()
            {
                Code = req.Code,
                NameVI = req.NameVI,
                NameEN = req.NameEN ?? ""
            };

            await _db.TblDmcustomerTypes.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateDepartment(CreateDepartmentRequest req)
        {
            var department = new TblDmphongBan()
            {
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
                IddmvanPhong = req.IdVanPhong,
            };

            await _db.TblDmphongBans.AddAsync(department);
            await _db.SaveChangesAsync();
        }

        public async Task CreateMajor(CreateMajorRequest req)
        {
            var data = new TblDmnghiepVu()
            {
                Code = req.Code,
                NameVI = req.NameVI,
                NameEN = req.NameEN ?? ""
            };

            await _db.TblDmnghiepVus.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateOffice(CreateOfficeRequest req)
        {
            var data = new TblDmvanPhong()
            {
                Code = req.Code,
                NameVI = req.NameVI,
                NameEN = req.NameEN ?? "",
                Idcity = (req.IdCity != null && req.IdCity != -1) ? req.IdCity : null,
                Idcountry = (req.IdCountry != null && req.IdCountry != -1) ? req.IdCountry : null,
                AddressVI = req.AddressVI ?? "",
                AddressEN = req.AddressEN ?? "",
                Phone = req.Phone ?? "",
                Fax = req.Fax ?? "",
                Email = req.Email ?? "",
                Website = req.Website ?? "",
                Note = req.Note ?? "",
                TaxCode = req.TaxCode ?? "",
            };

            await _db.TblDmvanPhongs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateOperational(CreateOperationalRequest req)
        {
            var data = new TblDmloaiTacNghiep()
            {
                Name = req.Name,
                R = req.R,
                G = req.G,
                B = req.B,
                NgayTuTraKhac = req.NgayTuTraKhach
            };

            await _db.TblDmloaiTacNghieps.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreatePort(CreatePortRequest req)
        {
            var data = new TblDmport()
            {
                IdquocGia = req.IdQuocGia != -1 ? req.IdQuocGia : null,
                Idcity = req.IdCity != -1 ? req.IdCity : null,
                Code = req.Code ?? "",
                TaxCode = req.TaxCode ?? "",
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
                AddressVI = req.AddressVI ?? "",
                AddressEN = req.AddressEN ?? "",
                Note = req.Note ?? "",
                Phone = req.Phone ?? "",
                Fax = req.Fax ?? "",
                Email = req.Email ?? "",
                Website = req.Website ?? "",
            };

            await _db.TblDmports.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreatePosition(CreatePositionRequest req)
        {
            var position = new TblDmchucVu()
            {
                Code = req.Code,
                NameVI = req.NameVI ?? "",
                NameEN = req.NameEN ?? "",
            };

            await _db.TblDmchucVus.AddAsync(position);
            await _db.SaveChangesAsync();
        }

        public async Task CreateTransportation(CreateTransportationRequest req)
        {
            var data = new TblDmloaiHinhVanChuyen()
            {
                Code = req.Code,
                NameVI = req.NameVI,
                NameEN = req.NameEN ?? ""
            };

            await _db.TblDmloaiHinhVanChuyens.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task CreateTypeOfCustomer(CreateTypeOfCustomerRequest req)
        {
            var data = new TblDmphanLoaiKhachHang()
            {
                Code = req.Code,
                NameVI = req.NameVI,
                NameEN = req.NameEN ?? ""
            };

            await _db.TblDmphanLoaiKhachHangs.AddAsync(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBusiness(TblDmloaiDoanhNghiep data)
        {
            _db.TblDmloaiDoanhNghieps.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCity(TblDmcity data)
        {
            _db.TblDmcities.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCountry(TblDmcountry data)
        {
            _db.TblDmcountries.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCustomerType(TblDmcustomerType data)
        {
            _db.TblDmcustomerTypes.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteDepartment(TblDmphongBan data)
        {
            _db.TblDmphongBans.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMajor(TblDmnghiepVu data)
        {
            _db.TblDmnghiepVus.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOffice(TblDmvanPhong data)
        {
            _db.TblDmvanPhongs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOperational(TblDmloaiTacNghiep data)
        {
            _db.TblDmloaiTacNghieps.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePort(TblDmport data)
        {
            _db.TblDmports.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePosition(TblDmchucVu data)
        {
            _db.TblDmchucVus.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTransportation(TblDmloaiHinhVanChuyen data)
        {
            _db.TblDmloaiHinhVanChuyens.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTypeOfCustomer(TblDmphanLoaiKhachHang data)
        {
            _db.TblDmphanLoaiKhachHangs.Remove(data);
            await _db.SaveChangesAsync();
        }

        public async Task<List<BusinessDto>?> GetAllBusinesses()
        {
            List<BusinessDto>? data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
            {
                Id = x.Id,
                Code = x.Code ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
            }).ToListAsync();

            return data;
        }

        public async Task<List<CityDto>?> GetAllCities()
        {
            List<CityDto>? data = await _db.TblDmcities.Select(c => new CityDto()
            {
                Id = c.Id,
                Code = c.Code ?? "",
                IdQuocGia = c.IdquocGia,
                NameVI = c.NameVI ?? "",
                NameEN = c.NameEN ?? "",
                FlagFavorite = c.FlagFavorite,
                Note = c.Note,
                QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
            }).ToListAsync();

            return data;
        }

        public async Task<List<CountryDto>?> GetAllCountries()
        {
            List<CountryDto>? data = await _db.TblDmcountries.Select(c => new CountryDto()
            {
                Id = c.Id,
                Code = c.Code ?? "",
                NameVI = c.NameVI ?? "",
                NameEN = c.NameEN ?? "",
                Note = c.Note ?? "",
                FlagFavorite = c.FlagFavorite ?? false
            }).ToListAsync();

            return data;
        }

        public async Task<List<CustomerTypeDto>?> GetAllCustomerTypes()
        {
            List<CustomerTypeDto>? data = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
            {
                Id = x.Id,
                Code = x.Code ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? ""
            }).ToListAsync();

            return data;
        }

        public async Task<List<DepartmentDto>?> GetAllDepartments()
        {
            List<DepartmentDto>? data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
            {
                Id = x.Id,
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
                IdVanPhong = x.IddmvanPhong ?? -1,
                GhiChu = x.GhiChu ?? "",
                FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<MajorDto>?> GetAllMajors()
        {
            List<MajorDto>? data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
            {
                Id = x.Id,
                Code = x.Code ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? ""
            }).ToListAsync();

            return data;
        }

        public async Task<List<OfficeDto>?> GetAllOffices()
        {
            List<OfficeDto>? data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
            {
                Id = x.Id,
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
                Code = x.Code ?? "",
                IdCountry = x.Idcountry,
                IdCity = x.Idcity,
                Fax = x.Fax ?? "",
                AddressVI = x.AddressVI ?? "",
                QuocGia = (x.IdcountryNavigation != null && x.IdcountryNavigation.NameVI != null) ? x.IdcountryNavigation.NameVI : "",
                ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                AddressEN = x.AddressEN ?? "",
                Email = x.Email ?? "",
                Note = x.Note ?? "",
                Phone = x.Phone ?? "",
                TaxCode = x.TaxCode ?? "",
                Website = x.Website ?? "",
                FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<OperationalDto>?> GetAllOperationals()
        {
            List<OperationalDto>? data = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
            {
                Id = x.Id,
                Name = x.Name ?? "",
                R = x.R,
                G = x.G,
                B = x.B,
                NgayTuTraKhach = x.NgayTuTraKhac
            }).ToListAsync();
            return data;
        }

        public async Task<List<PositionDto>?> GetAllPositions()
        {
            List<PositionDto>? data = await _db.TblDmchucVus.Select(x => new PositionDto()
            {
                Id = x.Id,
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
                Code = x.Code ?? "",
                FlagFavorite = x.FlagFavorite ?? false,
            }).ToListAsync();

            return data;
        }

        public async Task<List<TransportationDto>?> GetAllTransportations()
        {
            List<TransportationDto>? data = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
            {
                Id = x.Id,
                Code = x.Code ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? ""
            }).ToListAsync();

            return data;
        }

        public async Task<List<TypeOfCustomerDto>?> GetAllTypeOfCustomers()
        {
            List<TypeOfCustomerDto>? data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
            {
                Id = x.Id,
                Code = x.Code ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? ""
            }).ToListAsync();

            return data;
        }

        public async Task<TblDmloaiDoanhNghiep?> GetBusinessById(long id)
        {
            TblDmloaiDoanhNghiep? data = await _db.TblDmloaiDoanhNghieps.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<BusinessDto>?> GetBusinesses(int Start = 0, int Size = 10, string Search = "")
        {
            List<BusinessDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(x => (x.Code != null && x.Code.Contains(Search)) ||
                            (x.NameVI != null && x.NameVI.Contains(Search)) ||
                            (x.NameEN != null && x.NameEN.Contains(Search))
                )
                .ToListAsync();
            }

            return data;
        }

        public async Task<List<CityDto>?> GetCities(int Start = 0, int Size = 10, string Search = "")
        {
            List<CityDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcities.Select(c => new CityDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    IdQuocGia = c.IdquocGia,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    FlagFavorite = c.FlagFavorite,
                    Note = c.Note,
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcities.Select(c => new CityDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    IdQuocGia = c.IdquocGia,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    FlagFavorite = c.FlagFavorite,
                    Note = c.Note,
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                }).Where(x =>
                    (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                    (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search)) ||
                    (x.QuocGia != null && x.QuocGia.Contains(Search))
                ).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<List<CityDto>?> GetCitiesByIdCountry(long idQuocGia)
        {
            List<CityDto>? data = await _db.TblDmcities.Select(c => new CityDto()
            {
                Id = c.Id,
                Code = c.Code ?? "",
                IdQuocGia = c.IdquocGia,
                NameVI = c.NameVI ?? "",
                NameEN = c.NameEN ?? "",
                FlagFavorite = c.FlagFavorite,
                Note = c.Note,
                QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
            }).Where(c => c.IdQuocGia == idQuocGia).ToListAsync();

            return data;
        }

        public async Task<TblDmcity?> GetCityById(long id)
        {
            TblDmcity? data = await _db.TblDmcities.Where(c => c.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CountryDto>?> GetCountries(int Start = 0, int Size = 0, string Search = "")
        {
            List<CountryDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcountries.Select(c => new CountryDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    Note = c.Note ?? "",
                    FlagFavorite = c.FlagFavorite ?? false
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcountries.Select(c => new CountryDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    Note = c.Note ?? "",
                    FlagFavorite = c.FlagFavorite ?? false
                })
                .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                            (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                )
                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmcountry?> GetCountryById(long id)
        {
            TblDmcountry? data = await _db.TblDmcountries.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<TblDmcustomerType?> GetCustomerTypeById(long id)
        {
            TblDmcustomerType? data = await _db.TblDmcustomerTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<CustomerTypeDto>?> GetCustomerTypes(int Start = 0, int Size = 10, string Search = "")
        {
            List<CustomerTypeDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmphongBan?> GetDepartmentById(long id)
        {
            TblDmphongBan? data = await _db.TblDmphongBans.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<DepartmentDto>?> GetDepartments(int Start = 0, int Size = 10, string Search = "")
        {
            List<DepartmentDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(
                    x => (x.NameVI != null && x.NameVI.Contains(Search)) ||
                        (x.NameEN != null && x.NameEN.Contains(Search)) ||
                        (x.GhiChu != null && x.GhiChu.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmnghiepVu?> GetMajorById(long id)
        {
            TblDmnghiepVu? data = await _db.TblDmnghiepVus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<MajorDto>?> GetMajors(int Start = 0, int Size = 10, string Search = "")
        {
            List<MajorDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                                               .Where(
                                                   x => (x.Code != null && x.Code.Contains(Search)) ||
                                                   (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                                               ).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmvanPhong?> GetOfficeById(long id)
        {
            TblDmvanPhong? data = await _db.TblDmvanPhongs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<OfficeDto>?> GetOffices(int Start = 0, int Size = 10, string Search = "")
        {
            List<OfficeDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    IdCountry = x.Idcountry,
                    IdCity = x.Idcity,
                    Fax = x.Fax ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    QuocGia = (x.IdcountryNavigation != null && x.IdcountryNavigation.NameVI != null) ? x.IdcountryNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Phone = x.Phone ?? "",
                    TaxCode = x.TaxCode ?? "",
                    Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    IdCountry = x.Idcountry,
                    IdCity = x.Idcity,
                    Fax = x.Fax ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    QuocGia = (x.IdcountryNavigation != null && x.IdcountryNavigation.NameVI != null) ? x.IdcountryNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Phone = x.Phone ?? "",
                    TaxCode = x.TaxCode ?? "",
                    Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(
                    x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                        (x.Code != null && x.Code.Contains(Search)) || (x.AddressVI != null && x.AddressVI.Contains(Search)) ||
                        (x.AddressEN != null && x.AddressEN.Contains(Search)) || (x.Email != null && x.Email.Contains(Search)) ||
                        (x.Note != null && x.Note.Contains(Search)) || (x.Phone != null && x.Phone.Contains(Search)) ||
                        (x.TaxCode != null && x.TaxCode.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmloaiTacNghiep?> GetOperationalById(long id)
        {
            TblDmloaiTacNghiep? data = await _db.TblDmloaiTacNghieps.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<OperationalDto>?> GetOperationals(int Start = 0, int Size = 10, string Search = "")
        {
            List<OperationalDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id,
                    Name = x.Name ?? "",
                    R = x.R,
                    G = x.G,
                    B = x.B,
                    NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id,
                    Name = x.Name ?? "",
                    R = x.R,
                    G = x.G,
                    B = x.B,
                    NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(x => x.Name != null && x.Name.Contains(Search)).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmport?> GetPortById(long id)
        {
            TblDmport? data = await _db.TblDmports.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<PortDto>?> GetPorts(int Start = 0, int Size = 0, string Search = "")
        {
            List<PortDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmports.Select(x => new PortDto()
                {
                    Id = x.Id,
                    IdQuocGia = x.IdquocGia,
                    IdCity = x.Idcity,
                    QuocGia = (x.IdquocGiaNavigation != null && x.IdquocGiaNavigation.NameVI != null) ? x.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Code = x.Code ?? "",
                    TaxCode = x.TaxCode ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Phone = x.Phone ?? "",
                    Fax = x.Fax ?? "",
                    Email = x.Email ?? "",
                    Website = x.Website ?? "",
                    Note = x.Note ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmports.Select(x => new PortDto()
                {
                    Id = x.Id,
                    IdQuocGia = x.IdquocGia,
                    IdCity = x.Idcity,
                    QuocGia = (x.IdquocGiaNavigation != null && x.IdquocGiaNavigation.NameVI != null) ? x.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Code = x.Code ?? "",
                    TaxCode = x.TaxCode ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Phone = x.Phone ?? "",
                    Fax = x.Fax ?? "",
                    Email = x.Email ?? "",
                    Website = x.Website ?? "",
                    Note = x.Note ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(x =>
                            (x.QuocGia != null && x.QuocGia.Contains(Search)) || (x.ThanhPho != null && x.ThanhPho.Contains(Search)) ||
                            (x.Code != null && x.Code.Contains(Search)) || (x.TaxCode != null && x.TaxCode.Contains(Search)) ||
                            (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                            (x.AddressVI != null && x.AddressVI.Contains(Search)) || (x.AddressEN != null && x.AddressEN.Contains(Search)) ||
                            (x.Email != null && x.Email.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task<List<PortDto>?> GetPortsByIdCountry(long idCountry)
        {
            List<PortDto>? data = await _db.TblDmports.Select(x => new PortDto()
            {
                Id = x.Id,
                IdQuocGia = x.IdquocGia,
                IdCity = x.Idcity,
                QuocGia = (x.IdquocGiaNavigation != null && x.IdquocGiaNavigation.NameVI != null) ? x.IdquocGiaNavigation.NameVI : "",
                ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                Code = x.Code ?? "",
                TaxCode = x.TaxCode ?? "",
                NameVI = x.NameVI ?? "",
                NameEN = x.NameEN ?? "",
                AddressVI = x.AddressVI ?? "",
                AddressEN = x.AddressEN ?? "",
                Phone = x.Phone ?? "",
                Fax = x.Fax ?? "",
                Email = x.Email ?? "",
                Website = x.Website ?? "",
                Note = x.Note ?? ""
            }).Where(x => x.IdQuocGia == idCountry).ToListAsync();

            return data;
        }

        public async Task<TblDmchucVu?> GetPositionById(long id)
        {
            TblDmchucVu? data = await _db.TblDmchucVus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<PositionDto>?> GetPositions(int Start, int Size, string Search)
        {
            List<PositionDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                        (x.NameVI != null && x.NameVI.Contains(Search)) ||
                        (x.NameEN != null && x.NameEN.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task<int> GetTotalBusinesses(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmloaiDoanhNghieps.Select(x => new BusinessDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                }).Where(x => (x.Code != null && x.Code.Contains(Search)) ||
                            (x.NameVI != null && x.NameVI.Contains(Search)) ||
                            (x.NameEN != null && x.NameEN.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCities(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcities.Select(c => new CityDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    IdQuocGia = c.IdquocGia,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    FlagFavorite = c.FlagFavorite,
                    Note = c.Note,
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmcities.Select(c => new CityDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    IdQuocGia = c.IdquocGia,
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    FlagFavorite = c.FlagFavorite,
                    Note = c.Note,
                    QuocGia = (c.IdquocGiaNavigation != null && c.IdquocGiaNavigation.NameVI != null) ? c.IdquocGiaNavigation.NameVI : "",
                }).Where(x =>
                    (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                    (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search)) ||
                    (x.QuocGia != null && x.QuocGia.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCountries(string Search = "")
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcountries.Select(c => new CountryDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    Note = c.Note ?? "",
                    FlagFavorite = c.FlagFavorite ?? false
                }).OrderByDescending(x => x.Id).CountAsync();
            }
            else
            {
                total = await _db.TblDmcountries.Select(c => new CountryDto()
                {
                    Id = c.Id,
                    Code = c.Code ?? "",
                    NameVI = c.NameVI ?? "",
                    NameEN = c.NameEN ?? "",
                    Note = c.Note ?? "",
                    FlagFavorite = c.FlagFavorite ?? false
                })
                .Where(x => (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                            (x.NameEN != null && x.NameEN.Contains(Search)) || (x.Note != null && x.Note.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalCustomerTypes(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmcustomerTypes.Select(x => new CustomerTypeDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new CustomerTypeDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                })
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalDepartments(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmphongBans.Select(x => new DepartmentDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    IdVanPhong = x.IddmvanPhong ?? -1,
                    GhiChu = x.GhiChu ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                })
                .Where(
                    x => (x.NameVI != null && x.NameVI.Contains(Search)) ||
                         (x.NameEN != null && x.NameEN.Contains(Search)) ||
                         (x.GhiChu != null && x.GhiChu.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalMajors(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmnghiepVus.Select(x => new MajorDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                })
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalOffices(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    IdCountry = x.Idcountry,
                    IdCity = x.Idcity,
                    Fax = x.Fax ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    QuocGia = (x.IdcountryNavigation != null && x.IdcountryNavigation.NameVI != null) ? x.IdcountryNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Phone = x.Phone ?? "",
                    TaxCode = x.TaxCode ?? "",
                    Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmvanPhongs.Select(x => new OfficeDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    IdCountry = x.Idcountry,
                    IdCity = x.Idcity,
                    Fax = x.Fax ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Email = x.Email ?? "",
                    Note = x.Note ?? "",
                    QuocGia = (x.IdcountryNavigation != null && x.IdcountryNavigation.NameVI != null) ? x.IdcountryNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Phone = x.Phone ?? "",
                    TaxCode = x.TaxCode ?? "",
                    Website = x.Website ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                })
                .Where(
                    x => (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                        (x.Code != null && x.Code.Contains(Search)) || (x.AddressVI != null && x.AddressVI.Contains(Search)) ||
                        (x.AddressEN != null && x.AddressEN.Contains(Search)) || (x.Email != null && x.Email.Contains(Search)) ||
                        (x.Note != null && x.Note.Contains(Search)) || (x.Phone != null && x.Phone.Contains(Search)) ||
                        (x.TaxCode != null && x.TaxCode.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalOperationals(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id,
                    Name = x.Name ?? "",
                    R = x.R,
                    G = x.G,
                    B = x.B,
                    NgayTuTraKhach = x.NgayTuTraKhac
                }).OrderByDescending(x => x.Id).CountAsync();
            }
            else
            {
                total = await _db.TblDmloaiTacNghieps.Select(x => new OperationalDto()
                {
                    Id = x.Id,
                    Name = x.Name ?? "",
                    R = x.R,
                    G = x.G,
                    B = x.B,
                    NgayTuTraKhach = x.NgayTuTraKhac
                }).Where(x => x.Name != null && x.Name.Contains(Search)).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalPorts(string Search = "")
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmports.Select(x => new PortDto()
                {
                    Id = x.Id,
                    IdQuocGia = x.IdquocGia,
                    IdCity = x.Idcity,
                    QuocGia = (x.IdquocGiaNavigation != null && x.IdquocGiaNavigation.NameVI != null) ? x.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Code = x.Code ?? "",
                    TaxCode = x.TaxCode ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Phone = x.Phone ?? "",
                    Fax = x.Fax ?? "",
                    Email = x.Email ?? "",
                    Website = x.Website ?? "",
                    Note = x.Note ?? ""
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmports.Select(x => new PortDto()
                {
                    Id = x.Id,
                    IdQuocGia = x.IdquocGia,
                    IdCity = x.Idcity,
                    QuocGia = (x.IdquocGiaNavigation != null && x.IdquocGiaNavigation.NameVI != null) ? x.IdquocGiaNavigation.NameVI : "",
                    ThanhPho = (x.IdcityNavigation != null && x.IdcityNavigation.NameVI != null) ? x.IdcityNavigation.NameVI : "",
                    Code = x.Code ?? "",
                    TaxCode = x.TaxCode ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    AddressVI = x.AddressVI ?? "",
                    AddressEN = x.AddressEN ?? "",
                    Phone = x.Phone ?? "",
                    Fax = x.Fax ?? "",
                    Email = x.Email ?? "",
                    Website = x.Website ?? "",
                    Note = x.Note ?? ""
                })
                .Where(x =>
                            (x.QuocGia != null && x.QuocGia.Contains(Search)) || (x.ThanhPho != null && x.ThanhPho.Contains(Search)) ||
                            (x.Code != null && x.Code.Contains(Search)) || (x.TaxCode != null && x.TaxCode.Contains(Search)) ||
                            (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search)) ||
                            (x.AddressVI != null && x.AddressVI.Contains(Search)) || (x.AddressEN != null && x.AddressEN.Contains(Search)) ||
                            (x.Email != null && x.Email.Contains(Search)) || (x.Website != null && x.Website.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalPositions(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmchucVus.Select(x => new PositionDto()
                {
                    Id = x.Id,
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? "",
                    Code = x.Code ?? "",
                    FlagFavorite = x.FlagFavorite ?? false,
                })
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                        (x.NameVI != null && x.NameVI.Contains(Search)) ||
                        (x.NameEN != null && x.NameEN.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalTransportations(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                })
                .Where(x =>
                    (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                    (x.NameEN != null && x.NameEN.Contains(Search))
                )
                .CountAsync();
            }

            return total;
        }

        public async Task<int> GetTotalTypeOfCustomers(string Search)
        {
            var total = 0;

            if (Search == "")
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).CountAsync();
            }
            else
            {
                total = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                })
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                ).CountAsync();
            }

            return total;
        }

        public async Task<TblDmloaiHinhVanChuyen?> GetTransportationById(long id)
        {
            TblDmloaiHinhVanChuyen? data = await _db.TblDmloaiHinhVanChuyens.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<TransportationDto>?> GetTransportations(int Start = 0, int Size = 10, string Search = "")
        {
            List<TransportationDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmloaiHinhVanChuyens.Select(x => new TransportationDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                })
                .Where(x =>
                    (x.Code != null && x.Code.Contains(Search)) || (x.NameVI != null && x.NameVI.Contains(Search)) ||
                    (x.NameEN != null && x.NameEN.Contains(Search))
                )
                .OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }

            return data;
        }

        public async Task<TblDmphanLoaiKhachHang?> GetTypeOfCustomerById(long id)
        {
            TblDmphanLoaiKhachHang? data = await _db.TblDmphanLoaiKhachHangs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<TypeOfCustomerDto>?> GetTypeOfCustomers(int Start = 0, int Size = 10, string Search = "")
        {
            List<TypeOfCustomerDto>? data;

            if (Search == "")
            {
                data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size).ToListAsync();
            }
            else
            {
                data = await _db.TblDmphanLoaiKhachHangs.Select(x => new TypeOfCustomerDto()
                {
                    Id = x.Id,
                    Code = x.Code ?? "",
                    NameVI = x.NameVI ?? "",
                    NameEN = x.NameEN ?? ""
                }).OrderByDescending(x => x.Id).Skip(Start).Take(Size)
                .Where(
                    x => (x.Code != null && x.Code.Contains(Search)) ||
                    (x.NameVI != null && x.NameVI.Contains(Search)) || (x.NameEN != null && x.NameEN.Contains(Search))
                ).ToListAsync();
            }

            return data;
        }

        public async Task UpdateBusiness(TblDmloaiDoanhNghiep data, UpdateBusinessRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCity(TblDmcity data, UpdateCityRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.IdquocGia = req.IdQuocGia ?? data.IdquocGia;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCountry(TblDmcountry data, UpdateCountryRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateCustomerType(TblDmcustomerType data, UpdateCustomerTypeRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateDepartment(TblDmphongBan data, UpdateDepartmentRequest req)
        {
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;
            data.IddmvanPhong = req.IdVanPhong ?? data.IddmvanPhong;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateMajor(TblDmnghiepVu data, UpdateMajorRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateOffice(TblDmvanPhong data, UpdateOfficeRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;
            data.AddressVI = req.AddressVI ?? data.AddressVI;
            data.AddressEN = req.AddressEN ?? data.AddressEN;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.Idcountry = (req.IdCountry != null && req.IdCountry != -1) ? req.IdCountry : data.Idcountry;
            data.Idcity = (req.IdCity != null && req.IdCity != -1) ? req.IdCity : data.Idcity;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateOperational(TblDmloaiTacNghiep data, UpdateOperationalRequest req)
        {
            data.Name = req.Name ?? data.Name;
            data.R = req.R ?? data.R;
            data.G = req.G ?? data.G;
            data.B = req.B ?? data.B;
            data.NgayTuTraKhac = req.NgayTuTraKhach ?? data.NgayTuTraKhac;

            await _db.SaveChangesAsync();
        }

        public async Task UpdatePort(TblDmport data, UpdatePortRequest req)
        {
            data.IdquocGia = req.IdQuocGia ?? data.IdquocGia;
            data.Idcity = req.IdCity ?? data.Idcity;
            data.Code = req.Code ?? data.Code;
            data.TaxCode = req.TaxCode ?? data.TaxCode;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;
            data.AddressVI = req.AddressVI ?? data.AddressVI;
            data.AddressEN = req.AddressEN ?? data.AddressEN;
            data.Phone = req.Phone ?? data.Phone;
            data.Fax = req.Fax ?? data.Fax;
            data.Email = req.Email ?? data.Email;
            data.Website = req.Website ?? data.Website;
            data.Note = req.Note ?? data.Note;

            await _db.SaveChangesAsync();
        }

        public async Task UpdatePosition(TblDmchucVu data, UpdatePositionRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateTransportation(TblDmloaiHinhVanChuyen data, UpdateTransportationRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateTypeOfCustomer(TblDmphanLoaiKhachHang data, UpdateTypeOfCustomerRequest req)
        {
            data.Code = req.Code ?? data.Code;
            data.NameVI = req.NameVI ?? data.NameVI;
            data.NameEN = req.NameEN ?? data.NameEN;

            await _db.SaveChangesAsync();
        }
    }
}
