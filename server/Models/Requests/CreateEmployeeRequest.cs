namespace vsl_crm_api.Models.Requests
{
    public class CreateEmployeeRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Permission { get; set; }
        public long? IdChucVu { get; set; }
        public long? IdPhongBan { get; set; }
        public long? IdVanPhong { get; set; }
        public required string manhanvien { get; set; }
        public required string HoTenVI { get; set; }
        public string? HoTenEN { get; set; }
        public string? NamSinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? QueQuan { get; set; }
        public string? DiaChi { get; set; }
        public string? SoCMT { get; set; }
        public string? NoiCapCMT { get; set; }
        public string? NgayCapCMT { get; set; }
        public string? PhotoURL { get; set; }
        public string? GhiChu { get; set; }
        public int? SoLuongKH { get; set; }
    }
}
