using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmport
{
    public long Id { get; set; }

    public int? EnumPortType { get; set; }

    public long? IdquocGia { get; set; }

    public long? Idcity { get; set; }

    public string? Code { get; set; }

    public string? TaxCode { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public string? AddressVI { get; set; }

    public string? AddressEN { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public int? Rating { get; set; }

    public string? Note { get; set; }

    public double? MaxMoneyDebit { get; set; }

    public int? PaymentPeriod { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? Idbank { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankBranchName { get; set; }

    public string? BankAddress { get; set; }

    public double? SoDuVnd { get; set; }

    public double? SoDuUsd { get; set; }

    public double? PhanTramPhaiThuChi { get; set; }

    public virtual TblDmbank? IdbankNavigation { get; set; }

    public virtual TblDmcity? IdcityNavigation { get; set; }

    public virtual TblDmcountry? IdquocGiaNavigation { get; set; }

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExIdfromPortNavigations { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExIdtoPortNavigations { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblDmcontactList> TblDmcontactLists { get; set; } = new List<TblDmcontactList>();

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangIdcangDenNavigations { get; set; } = new List<TblDmcustomerTuyenHang>();

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangIdcangDiNavigations { get; set; } = new List<TblDmcustomerTuyenHang>();
}
