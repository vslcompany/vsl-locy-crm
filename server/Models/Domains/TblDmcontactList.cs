using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcontactList
{
    public long Id { get; set; }

    public long? IdcontactFun { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public string? AddressVI { get; set; }

    public string? AddressEN { get; set; }

    public int? EnumGioiTinh { get; set; }

    public string? HandPhone { get; set; }

    public string? HomePhone { get; set; }

    public string? Email { get; set; }

    public string? Note { get; set; }

    public int? EnumAgentCompanyType { get; set; }

    public long? Idcustomer { get; set; }

    public long? Idport { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? Idbank { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankBranchName { get; set; }

    public string? BankAddress { get; set; }

    public string? Chat { get; set; }

    public bool? FlagActive { get; set; }

    public bool? FlagDaiDien { get; set; }

    public string? ChucVu { get; set; }

    public virtual TblDmbank? IdbankNavigation { get; set; }

    public virtual TblDmcontactFun? IdcontactFunNavigation { get; set; }

    public virtual TblDmcustomer? IdcustomerNavigation { get; set; }

    public virtual TblDmport? IdportNavigation { get; set; }

    public virtual ICollection<TblCustomerTacNghiep> TblCustomerTacNghieps { get; set; } = new List<TblCustomerTacNghiep>();
}
