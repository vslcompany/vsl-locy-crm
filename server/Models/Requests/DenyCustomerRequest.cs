namespace vsl_crm_api.Models.Requests
{
    public class DenyCustomerRequest
    {
        public required long IDNhanVienSale { get; set; }
        public required long[] IdCustomers { get; set; }
        public string? LyDoTuChoi { get; set; }
    }
}
