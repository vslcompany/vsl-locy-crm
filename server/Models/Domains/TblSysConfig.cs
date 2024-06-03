using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysConfig
{
    public long Id { get; set; }

    public string? Appkey { get; set; }

    public string? Serverip { get; set; }

    public string? Customername { get; set; }

    public string? Address { get; set; }

    public string? Telfax { get; set; }

    public string? Website { get; set; }

    public string? Email { get; set; }

    public string? Mst { get; set; }

    public string? HotLine { get; set; }

    public int? Flag { get; set; }

    public int? Flaguse { get; set; }

    public byte[]? Logo { get; set; }

    public byte[]? LetterHead { get; set; }

    public byte[]? ReportHeader { get; set; }

    public double? LogoW { get; set; }

    public double? LogoH { get; set; }

    public double? LogoLeft { get; set; }

    public double? LogoTop { get; set; }

    public double? ReportHeaderW { get; set; }

    public double? ReportHeaderH { get; set; }

    public double? ReportHeaderLeft { get; set; }

    public double? ReportHeaderTop { get; set; }

    public double? LetterHeaderW { get; set; }

    public double? LetterHeaderH { get; set; }

    public double? LetterHeaderLeft { get; set; }

    public double? LetterHeaderTop { get; set; }

    public double? LetterHeaderDistanceToLogo { get; set; }

    public string? CustomerNameVI { get; set; }

    public double? NumberUser { get; set; }

    public bool? FlagFontSize { get; set; }
}
