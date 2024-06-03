using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmnghiepVu
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public virtual ICollection<TblDmcustomerNghiepVu> TblDmcustomerNghiepVus { get; set; } = new List<TblDmcustomerNghiepVu>();
}
