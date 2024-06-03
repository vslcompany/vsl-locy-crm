using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmloaiNotification
{
    public long Id { get; set; }

    public string? TieuDeVi { get; set; }

    public string? TieuDeEn { get; set; }

    public string? NoiDungVi { get; set; }

    public string? NoiDungEn { get; set; }

    public string? Tag { get; set; }

    public bool? FlagFavorite { get; set; }
}
