using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmchucVu
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public bool? FlagFavorite { get; set; }

    public int? ShowOrder { get; set; }

    public virtual ICollection<TblNhanSu> TblNhanSus { get; set; } = new List<TblNhanSu>();
}
