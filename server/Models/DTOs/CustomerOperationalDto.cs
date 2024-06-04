namespace vsl_crm_api.Models.DTOs
{
    public class CustomerOperationalDto
    {
        public long Id { get; set; }
        public long? IdLoaiTacNghiep { get; set; }
        public string? LoaiTacNghiep { get; set; }
        public string? NoiDung { get; set; }
        public string? DateCreate { get; set; }
        public long? IdUserCreate { get; set; }
        public string? NguoiTao { get; set; }
        public long? IdCustomer { get; set; }
        public string? ThoiGianThucHien { get; set; }
        public long? IdNguoiLienHe { get; set; }
        public string? NguoiLienHe { get; set; }
        public string? KhachHangPhanHoi { get; set; }
        public string? NgayPhanHoi { get; set; }
    }
}
