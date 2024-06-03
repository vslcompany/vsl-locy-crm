using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcountry
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public string? Note { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExIdfromCountryNavigations { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExIdtoCountryNavigations { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblDmcity> TblDmcities { get; set; } = new List<TblDmcity>();

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangIdquocGiaDenNavigations { get; set; } = new List<TblDmcustomerTuyenHang>();

    public virtual ICollection<TblDmcustomerTuyenHang> TblDmcustomerTuyenHangIdquocGiaDiNavigations { get; set; } = new List<TblDmcustomerTuyenHang>();

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmport> TblDmports { get; set; } = new List<TblDmport>();

    public virtual ICollection<TblDmvanPhong> TblDmvanPhongs { get; set; } = new List<TblDmvanPhong>();
}
