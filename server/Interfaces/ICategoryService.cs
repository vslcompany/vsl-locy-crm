using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Requests;

namespace vsl_crm_api.Interfaces
{
    public interface ICategoryService
    {
        Task<List<PositionDto>?> GetAllPositions();
        Task<List<PositionDto>?> GetPositions(int Start, int Size, string Search);
        Task<int> GetTotalPositions(string Search);
        Task<TblDmchucVu?> GetPositionById(long id);
        Task CreatePosition(CreatePositionRequest req);
        Task UpdatePosition(TblDmchucVu data, UpdatePositionRequest req);
        Task DeletePosition(TblDmchucVu data);

        Task<List<DepartmentDto>?> GetAllDepartments();
        Task<List<DepartmentDto>?> GetDepartments(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalDepartments(string Search);
        Task<TblDmphongBan?> GetDepartmentById(long id);
        Task CreateDepartment(CreateDepartmentRequest req);
        Task UpdateDepartment(TblDmphongBan data, UpdateDepartmentRequest req);
        Task DeleteDepartment(TblDmphongBan data);

        Task<List<OfficeDto>?> GetAllOffices();
        Task<List<OfficeDto>?> GetOffices(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalOffices(string Search);
        Task<TblDmvanPhong?> GetOfficeById(long id);
        Task CreateOffice(CreateOfficeRequest req);
        Task UpdateOffice(TblDmvanPhong data, UpdateOfficeRequest req);
        Task DeleteOffice(TblDmvanPhong data);

        Task<List<CityDto>?> GetAllCities();
        Task<List<CityDto>?> GetCitiesByIdCountry(long idQuocGia);
        Task<TblDmcity?> GetCityById(long id);
        Task<List<CityDto>?> GetCities(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalCities(string Search);
        Task CreateCity(CreateCityRequest req);
        Task UpdateCity(TblDmcity data, UpdateCityRequest req);
        Task DeleteCity(TblDmcity data);

        Task<List<CountryDto>?> GetAllCountries();
        Task<TblDmcountry?> GetCountryById(long id);
        Task<List<CountryDto>?> GetCountries(int Start = 0, int Size = 0, string Search = "");
        Task<int> GetTotalCountries(string Search = "");
        Task CreateCountry(CreateCountryRequest req);
        Task UpdateCountry(TblDmcountry data, UpdateCountryRequest req);
        Task DeleteCountry(TblDmcountry data);

        Task<List<BusinessDto>?> GetAllBusinesses();
        Task<List<BusinessDto>?> GetBusinesses(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalBusinesses(string Search);
        Task<TblDmloaiDoanhNghiep?> GetBusinessById(long id);
        Task CreateBusiness(CreateBusinessRequest req);
        Task UpdateBusiness(TblDmloaiDoanhNghiep data, UpdateBusinessRequest req);
        Task DeleteBusiness(TblDmloaiDoanhNghiep data);

        Task<List<TransportationDto>?> GetAllTransportations();
        Task<List<TransportationDto>?> GetTransportations(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalTransportations(string Search);
        Task<TblDmloaiHinhVanChuyen?> GetTransportationById(long id);
        Task CreateTransportation(CreateTransportationRequest req);
        Task UpdateTransportation(TblDmloaiHinhVanChuyen data, UpdateTransportationRequest req);
        Task DeleteTransportation(TblDmloaiHinhVanChuyen data);

        Task<List<OperationalDto>?> GetAllOperationals();
        Task<List<OperationalDto>?> GetOperationals(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalOperationals(string Search);
        Task<TblDmloaiTacNghiep?> GetOperationalById(long id);
        Task CreateOperational(CreateOperationalRequest req);
        Task UpdateOperational(TblDmloaiTacNghiep data, UpdateOperationalRequest req);
        Task DeleteOperational(TblDmloaiTacNghiep data);

        Task<List<MajorDto>?> GetAllMajors();
        Task<List<MajorDto>?> GetMajors(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalMajors(string Search);
        Task<TblDmnghiepVu?> GetMajorById(long id);
        Task CreateMajor(CreateMajorRequest req);
        Task UpdateMajor(TblDmnghiepVu data, UpdateMajorRequest req);
        Task DeleteMajor(TblDmnghiepVu data);

        Task<List<TypeOfCustomerDto>?> GetAllTypeOfCustomers();
        Task<List<TypeOfCustomerDto>?> GetTypeOfCustomers(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalTypeOfCustomers(string Search);
        Task<TblDmphanLoaiKhachHang?> GetTypeOfCustomerById(long id);
        Task CreateTypeOfCustomer(CreateTypeOfCustomerRequest req);
        Task UpdateTypeOfCustomer(TblDmphanLoaiKhachHang data, UpdateTypeOfCustomerRequest req);
        Task DeleteTypeOfCustomer(TblDmphanLoaiKhachHang data);

        Task<List<PortDto>?> GetPorts(int Start = 0, int Size = 0, string Search = "");
        Task<int> GetTotalPorts(string Search = "");
        Task<List<PortDto>?> GetPortsByIdCountry(long idCountry);
        Task<TblDmport?> GetPortById(long id);
        Task CreatePort(CreatePortRequest req);
        Task UpdatePort(TblDmport data, UpdatePortRequest req);
        Task DeletePort(TblDmport data);

        Task<List<CustomerTypeDto>?> GetAllCustomerTypes();
        Task<List<CustomerTypeDto>?> GetCustomerTypes(int Start = 0, int Size = 10, string Search = "");
        Task<int> GetTotalCustomerTypes(string Search);
        Task<TblDmcustomerType?> GetCustomerTypeById(long id);
        Task CreateCustomerType(CreateCustomerTypeRequest req);
        Task UpdateCustomerType(TblDmcustomerType data, UpdateCustomerTypeRequest req);
        Task DeleteCustomerType(TblDmcustomerType data);
    }
}
