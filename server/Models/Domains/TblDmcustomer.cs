using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcustomer
{
    public long Id { get; set; }

    public long? IdquocGia { get; set; }

    public long? Idcity { get; set; }

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

    public int? Rating { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? Idbank { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankBranchName { get; set; }

    public string? BankAddress { get; set; }

    public bool? FlagCrm { get; set; }

    public long? IdnhanVienSale { get; set; }

    public long? IduserCreate { get; set; }

    public DateTime? DateCreate { get; set; }

    public bool? FlagActive { get; set; }

    public string? MaChiNhanh { get; set; }

    public int? EnumLoaiKhachHang { get; set; }

    public bool? FlagDel { get; set; }

    public long? IdloaiDoanhNghiep { get; set; }

    public long? IduserDelete { get; set; }

    public DateTime? DateDelete { get; set; }

    public string? LyDoXoa { get; set; }

    public DateTime? NgayTuongTac { get; set; }

    public DateTime? NgayChonKhach { get; set; }

    public DateTime? NgayTraVe { get; set; }

    public DateTime? NgayChotKhach { get; set; }

    public int? SttmaxTacNghiep { get; set; }

    public DateTime? NgayTacNghiep { get; set; }

    public int? EnumGiaoNhan { get; set; }

    public DateTime? NgayGiao { get; set; }

    public DateTime? NgayNhan { get; set; }

    public long? IduserGiaoViec { get; set; }

    public long? IduserTraKhach { get; set; }

    public DateTime? NgayKetThucNhan { get; set; }

    public string? ListTacNghiepText { get; set; }

    public string? ListTuyenHangText { get; set; }

    public string? ListPhanHoiText { get; set; }

    public DateTime? NgayTuTraKhach { get; set; }

    public string? ThongTinGiaoViec { get; set; }

    public string? LyDoTuChoi { get; set; }

    public long? IdtacNghiepCuoi { get; set; }

    public string? MauTacNghiepCuoi { get; set; }

    public virtual TblDmbank? IdbankNavigation { get; set; }

    public virtual TblDmcity? IdcityNavigation { get; set; }

    public virtual TblDmloaiDoanhNghiep? IdloaiDoanhNghiepNavigation { get; set; }

    public virtual TblNhanSu? IdnhanVienSaleNavigation { get; set; }

    public virtual TblDmcountry? IdquocGiaNavigation { get; set; }

    public virtual TblSysUser? IduserCreateNavigation { get; set; }

    public virtual TblSysUser? IduserDeleteNavigation { get; set; }

    public virtual TblSysUser? IduserGiaoViecNavigation { get; set; }

    public virtual TblSysUser? IduserTraKhachNavigation { get; set; }

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExes { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblCustomerTacNghiep> TblCustomerTacNghieps { get; set; } = new List<TblCustomerTacNghiep>();

    public virtual ICollection<TblDmcontactList> TblDmcontactLists { get; set; } = new List<TblDmcontactList>();

    public virtual ICollection<TblDmcustomerDanhGium> TblDmcustomerDanhGia { get; set; } = new List<TblDmcustomerDanhGium>();

    public virtual ICollection<TblDmcustomerNghiepVu> TblDmcustomerNghiepVus { get; set; } = new List<TblDmcustomerNghiepVu>();

    public virtual ICollection<TblDmcustomerPhanLoaiKh> TblDmcustomerPhanLoaiKhs { get; set; } = new List<TblDmcustomerPhanLoaiKh>();

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangs { get; set; } = new List<TblDmcustomerTuyenHang>();
}
