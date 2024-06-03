using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblNhanSu
{
    public long Id { get; set; }

    public long? IdchucVu { get; set; }

    public long? IdphongBan { get; set; }

    public long? IdvanPhong { get; set; }

    public string Manhansu { get; set; } = null!;

    public string? Mst { get; set; }

    public string HoTenVI { get; set; } = null!;

    public string? HoTenEN { get; set; }

    public DateOnly NamSinh { get; set; }

    public int? GioiTinh { get; set; }

    public string? QueQuan { get; set; }

    public string? DiaChiThuongTru { get; set; }

    public string? DiaChiHienTai { get; set; }

    public string? SoCmt { get; set; }

    public string? NoiCapCmt { get; set; }

    public DateOnly? NgayCapCmt { get; set; }

    public string? SoHoChieu { get; set; }

    public string? NoiCapHoChieu { get; set; }

    public DateOnly? NgayCapHoChieu { get; set; }

    public DateOnly? NgayHetHanHoChieu { get; set; }

    public DateOnly? NgayKyHopDong { get; set; }

    public byte[]? PictureNv { get; set; }

    public string? SoTaiKhoanNh { get; set; }

    public string? DiDong { get; set; }

    public string? Email { get; set; }

    public string? SoTruong { get; set; }

    public int? EnumTinhTrangHonNhan { get; set; }

    public int? EnumNhomMau { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }

    public long? Luongcoban { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? EditDate { get; set; }

    public string? SoBangLai { get; set; }

    public DateTime? NgayHetHanBangLai { get; set; }

    public bool? FlagIsManager { get; set; }

    public string? SoHopDong { get; set; }

    public double? SoDuVnd { get; set; }

    public double? HanMucTamUng { get; set; }

    public int? SoLuongKh { get; set; }

    public bool? FlagDelete { get; set; }

    public long? IduserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public string? PhotoUrl { get; set; }

    public virtual TblDmchucVu? IdchucVuNavigation { get; set; }

    public virtual TblDmphongBan? IdphongBanNavigation { get; set; }

    public virtual TblDmvanPhong? IdvanPhongNavigation { get; set; }

    public virtual ICollection<TblBaoCaoCongViec> TblBaoCaoCongViecs { get; set; } = new List<TblBaoCaoCongViec>();

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmvanPhong> TblDmvanPhongIdcontactNavigations { get; set; } = new List<TblDmvanPhong>();

    public virtual ICollection<TblDmvanPhong> TblDmvanPhongIdkeToanTruongNavigations { get; set; } = new List<TblDmvanPhong>();

    public virtual ICollection<TblNhanSuTreelist> TblNhanSuTreelists { get; set; } = new List<TblNhanSuTreelist>();

    public virtual ICollection<TblSysFcmtoken> TblSysFcmtokens { get; set; } = new List<TblSysFcmtoken>();

    public virtual ICollection<TblSysUser> TblSysUsers { get; set; } = new List<TblSysUser>();
}
