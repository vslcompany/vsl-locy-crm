namespace vsl_crm_api.Models.Requests
{
    public class DeliveryCustomerRequest
    {
        public required long IDNhanVienSale { get; set; }
        public required long IDUserGiaoViec { get; set; }
        public required long[] IdCustomers { get; set; }
        public string? ThongTinGiaoViec { get; set; }
    }
}
