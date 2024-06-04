namespace vsl_crm_api.Models.DTOs
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public long? IdQuocGia { get; set; }
        public string? QuocGia { get; set; }
        public long? IdCity { get; set; }
        public string? ThanhPho { get; set; }
        public string? Code { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? AddressVI { get; set; }
        public string? AddressEN { get; set; }
        public string? TaxCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public long? IdNhanVienSale { get; set; }
        public string? NhanVien { get; set; }
        public long? IdUserCreate { get; set; }
        public string? NguoiTao { get; set; }
        public string? DateCreate { get; set; }
        public bool? FlagActive { get; set; }
        public int? EnumLoaiKhachHang { get; set; }
        public bool? FlagDel { get; set; }
        public long? IdLoaiDoanhNghiep { get; set; }
        public string? LoaiDoanhNghiep { get; set; }
        public long? IdUserDelete { get; set; }
        public string? NguoiXoa { get; set; }
        public string? DateDelete { get; set; }
        public string? LyDoXoa { get; set; }
        public string? NgayTuongTac { get; set; }
        public string? NgayChonKhach { get; set; }
        public string? NgayTraVe { get; set; }
        public string? NgayChotKhach { get; set; }
        public int? SttMaxTacNghiep { get; set; }
        public string? NgayTacNghiep { get; set; }
        public int? EnumGiaoNhan { get; set; }
        public string? NgayGiao { get; set; }
        public string? NgayNhan { get; set; }
        public long? IdUserGiaoViec { get; set; }
        public string? NguoiGiaoViec { get; set; }
        public long? IdUserTraKhach { get; set; }
        public string? NguoiTraKhach { get; set; }
        public string? NgayKetThucNhan { get; set; }
        public string? ListTacNghiepText { get; set; }
        public string? ListTuyenHangText { get; set; }
        public string? ListPhanHoiText { get; set; }
        public string? NgayTuTraKhach { get; set; }
        public string? ThongTinGiaoViec { get; set; }
        public string? LyDoTuChoi { get; set; }
        public long? IdTacNghiepCuoi { get; set; }
        public string? MauTacNghiepCuoi { get; set; }
    }
}
