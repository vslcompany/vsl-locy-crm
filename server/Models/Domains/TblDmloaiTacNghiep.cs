using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmloaiTacNghiep
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public bool? FlagFavorite { get; set; }

    public string? TenMau { get; set; }

    public int? G { get; set; }

    public int? R { get; set; }

    public int? B { get; set; }

    public int? Stt { get; set; }

    public int? NgayTuTraKhac { get; set; }

    public virtual ICollection<TblCustomerTacNghiep> TblCustomerTacNghieps { get; set; } = new List<TblCustomerTacNghiep>();
}
