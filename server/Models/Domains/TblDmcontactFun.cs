using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcontactFun
{
    public long Id { get; set; }

    public string? FunctionalVi { get; set; }

    public string? FunctionalEn { get; set; }

    public string? Note { get; set; }

    public bool? FlagFavorite { get; set; }

    public virtual ICollection<TblDmcontactList> TblDmcontactLists { get; set; } = new List<TblDmcontactList>();
}
