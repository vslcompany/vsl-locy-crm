namespace vsl_crm_api.Models.DTOs
{
    public class CustomerClassifyDto
    {
        public long Id { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdPhanLoaiKhachHang { get; set; }
        public string? PhanLoaiKhachHang { get; set; }
    }
}
