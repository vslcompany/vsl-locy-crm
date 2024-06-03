namespace vsl_crm_api.Models.DTOs
{
    public class ProfileDto
    {
        public long id { get; set; }
        public string? username { get; set; }
        public bool? active { get; set; }
        public string? permission { get; set; }
        public long? idNhanVien { get; set; }
        public long? idChucVu { get; set; }
        public string? chucVu { get; set; }
        public long? idPhongBan { get; set; }
        public string? phongban { get; set; }
        public long? idVanPhong { get; set; }
        public string? vanPhong { get; set; }
        public string? manhanvien { get; set; }
        public string? hoTenVI { get; set; }
        public string? hoTenEN { get; set; }
        public string? namsinh { get; set; }
        public int? gioitinh { get; set; }
        public string? quequan { get; set; }
        public string? diachi { get; set; }
        public string? soCMT { get; set; }
        public string? noiCapCMT { get; set; }
        public string? ngayCapCMT { get; set; }
        public string? photoURL { get; set; }
        public string? didong { get; set; }
        public string? email { get; set; }
        public string? ghichu { get; set; }
        public string? createDate { get; set; }
        public string? editDate { get; set; }
        public int? soLuongKH { get; set; }
        public bool? flagDelete { get; set; }
        public long? idUserDelete { get; set; }
        public string? dateDelete { get; set; }
    }
}
