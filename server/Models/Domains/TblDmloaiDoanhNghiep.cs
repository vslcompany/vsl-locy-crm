using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmloaiDoanhNghiep
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public virtual ICollection<TblDmcustomer> TblDmcustomers { get; set; } = new List<TblDmcustomer>();
}
