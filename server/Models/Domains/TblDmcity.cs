using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcity
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public long? IdquocGia { get; set; }

    public string? NameEN { get; set; }

    public string? NameVI { get; set; }

    public string? Note { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual TblDmcountry? IdquocGiaNavigation { get; set; }

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmport> TblDmports { get; set; } = new List<TblDmport>();

    public virtual ICollection<TblDmvanPhong> TblDmvanPhongs { get; set; } = new List<TblDmvanPhong>();
}
