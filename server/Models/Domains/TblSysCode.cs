using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysCode
{
    public long Id { get; set; }

    public int? EnumLoaiCongViec { get; set; }

    public bool? FlagActive { get; set; }

    public int? EnumKieuSinhMa { get; set; }

    public string? GhiChu { get; set; }

    public long? IduserCreate { get; set; }

    public long? IduserEdit { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateEdit { get; set; }

    public string? CodeFormat { get; set; }

    public int? EnumTangStttgian { get; set; }

    public int? EnumTangSttloaiHinh { get; set; }

    public int? SttbatDau { get; set; }

    public DateTime? NgayBdapDung { get; set; }

    public int? NgayBd { get; set; }

    public int? SoNgay { get; set; }

    public int? EnumLoaiKy { get; set; }

    public int? EnumNgayTinhStt { get; set; }

    public int? EnumTangSttcustomer { get; set; }

    public virtual ICollection<TblSysCodeKey> TblSysCodeKeys { get; set; } = new List<TblSysCodeKey>();

    public virtual ICollection<TblSysCodeKyHieu> TblSysCodeKyHieus { get; set; } = new List<TblSysCodeKyHieu>();
}
