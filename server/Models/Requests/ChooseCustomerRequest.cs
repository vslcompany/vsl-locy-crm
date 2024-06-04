namespace vsl_crm_api.Models.Requests
{
    public class ChooseCustomerRequest
    {
        public required long IdNhanVienSale { get; set; }
        public required long[] IdCustomers { get; set; }
    }
}
