using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysOption
{
    public long Id { get; set; }

    public int? Language { get; set; }

    public string? Jobidformat { get; set; }

    public int? UsddecimalCal { get; set; }

    public bool? SuDung { get; set; }

    public int? EnumKieuTangStt { get; set; }

    public double? TyGia { get; set; }

    public int? EnumCompany { get; set; }

    public int? EnumShowJobinfo { get; set; }

    public bool? FlagHideToDo { get; set; }

    public int? NgayBd { get; set; }

    public int? NgayKt { get; set; }

    public int? EnumAttachFileType { get; set; }

    public int? EnumJobid { get; set; }

    public int? SttbatDau { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public bool? FlagSetting { get; set; }

    public bool? FlagNewTab { get; set; }

    public bool? FlagSaveGridFilter { get; set; }

    public bool? FlagPheDuyetChi { get; set; }

    public int? SoLuongKh { get; set; }

    public int? NgayNhanKhach { get; set; }
}
