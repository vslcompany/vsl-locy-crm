using vsl_crm_api.Models.Domains;
using vsl_crm_api.Models.DTOs;
using vsl_crm_api.Models.Queries;
using vsl_crm_api.Models.Requests;
using vsl_crm_api.Models.Responses;

namespace vsl_crm_api.Interfaces
{
    public interface ICustomerService
    {
        // Manage data
        Task<QueryResponse<CustomerDto>> GetData(CustomerQuery query, string permission, long idUser, long idEmployee, List<long> idEmployees);
        Task<TblDmcustomer?> GetById(long id);
        Task Create(CreateCustomerRequest req);
        Task Update(TblDmcustomer data, UpdateCustomerRequest req);
        Task Delete(TblDmcustomer data, DeleteCustomerRequest req);
        Task Remove(TblDmcustomer data);
        Task<bool> IsExistCodeCustomer(string code);
        Task<bool> IsExistTaxCodeCustomer(string code);

        // Other
        Task<List<TblDmcustomer>?> GetCustomersByIdArray(long[] ids, long? IdNhanVien = null);
        Task ChooseCustomers(List<TblDmcustomer> data, ChooseCustomerRequest req);
        Task DeliveryCustomers(List<TblDmcustomer> data, DeliveryCustomerRequest req);
        Task UndeliveryCustomers(List<TblDmcustomer> data, UndeliveryCustomerRequest req);
        Task AcceptCustomers(List<TblDmcustomer> data, AcceptCustomerRequest req);
        Task DenyCustomers(List<TblDmcustomer> data, DenyCustomerRequest req);

        // Danh sách export
        Task<List<TblDmcustomer>?> GetCustomersData(int pageNumber, int pageSize, string permission, long idUser, long idEmployee, List<long> idEmployees);
        Task<List<TblDmcustomer>?> GetCustomersReceivedData(int pageNumber, int pageSize, string permission, long idUser, long idEmployee, List<long> idEmployees);
    }
}
