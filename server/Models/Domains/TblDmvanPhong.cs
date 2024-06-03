using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmvanPhong
{
    public long Id { get; set; }

    public string? Mst { get; set; }

    public long? IdkeToanTruong { get; set; }

    public string? Code { get; set; }

    public long? Idcountry { get; set; }

    public long? Idcity { get; set; }

    public string? NameEN { get; set; }

    public string? NameVI { get; set; }

    public string? AddressVI { get; set; }

    public string? AddressEN { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? Note { get; set; }

    public string? TaxCode { get; set; }

    public double? MaxMoneyDebit { get; set; }

    public int? Rating { get; set; }

    public int? PaymentPeriod { get; set; }

    public bool? FlagFavorite { get; set; }

    public long? ParentId { get; set; }

    public long? Idcontact { get; set; }

    public double? SoDuDauKy { get; set; }

    public double? TonCuoiKy { get; set; }

    public long? Idcurrency { get; set; }

    public DateOnly? CreateDate { get; set; }

    public virtual TblDmcity? IdcityNavigation { get; set; }

    public virtual TblNhanSu? IdcontactNavigation { get; set; }

    public virtual TblDmcountry? IdcountryNavigation { get; set; }

    public virtual TblDmcurrency? IdcurrencyNavigation { get; set; }

    public virtual TblNhanSu? IdkeToanTruongNavigation { get; set; }

    public virtual ICollection<TblDmbankAccount> TblDmbankAccounts { get; set; } = new List<TblDmbankAccount>();

    public virtual ICollection<TblDmphongBan> TblDmphongBans { get; set; } = new List<TblDmphongBan>();

    public virtual ICollection<TblNhanSu> TblNhanSus { get; set; } = new List<TblNhanSu>();
}
