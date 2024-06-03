using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmbankAccount
{
    public long Id { get; set; }

    public long? Idbank { get; set; }

    public string? BankAccount { get; set; }

    public string? BankOwner { get; set; }

    public string? BankName { get; set; }

    public string? BankBranchName { get; set; }

    public string? BankAddress { get; set; }

    public double? SoDuDauKy { get; set; }

    public double? SoDuCuoiKy { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? IdvanPhong { get; set; }

    public long? Idcurrency { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool? FlagTkgiamDoc { get; set; }

    public virtual TblDmbank? IdbankNavigation { get; set; }

    public virtual TblDmcurrency? IdcurrencyNavigation { get; set; }

    public virtual TblDmvanPhong? IdvanPhongNavigation { get; set; }
}
