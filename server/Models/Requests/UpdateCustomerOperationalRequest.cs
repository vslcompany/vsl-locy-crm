namespace vsl_crm_api.Models.Requests
{
    public class UpdateCustomerOperationalRequest
    {
        public long Id { get; set; }
        public long? IdLoaiTacNghiep { get; set; }
        public string? NoiDung { get; set; }
        public long? IdCustomer { get; set; }
        public string? ThoiGianThucHien { get; set; }
        public long? IdNguoiLienHe { get; set; }
        public string? KhachHangPhanHoi { get; set; }
        public string? NgayPhanHoi { get; set; }
    }
}
