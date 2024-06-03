using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmbank
{
    public long Id { get; set; }

    public string? BankName { get; set; }

    public string? Manager { get; set; }

    public byte[]? Logo { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Note { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual ICollection<TblDmbankAccount> TblDmbankAccounts { get; set; } = new List<TblDmbankAccount>();

    public virtual ICollection<TblDmcontactList> TblDmcontactLists { get; set; } = new List<TblDmcontactList>();

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmport> TblDmports { get; set; } = new List<TblDmport>();
}
