namespace vsl_crm_api.Models.Requests
{
    public class UpdateCustomerClassifyRequest
    {
        public long Id { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdPhanLoaiKhachHang { get; set; }
    }
}
