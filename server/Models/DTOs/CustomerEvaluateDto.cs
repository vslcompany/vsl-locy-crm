namespace vsl_crm_api.Models.DTOs
{
    public class CustomerEvaluateDto
    {
        public long Id { get; set; }
        public long? IdCustomer { get; set; }
        public long? IdCustomerType { get; set; }
        public string? LoaiKhachHang { get; set; }
        public long? IdUserCreate { get; set; }
        public string? NguoiTao { get; set; }
        public string? DateCreate { get; set; }
        public string? GhiChu { get; set; }
    }
}
