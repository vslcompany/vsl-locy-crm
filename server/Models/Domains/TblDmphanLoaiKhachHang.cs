using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmphanLoaiKhachHang
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? NameVI { get; set; }

    public string? NameEN { get; set; }

    public virtual ICollection<TblDmcustomerPhanLoaiKh> TblDmcustomerPhanLoaiKhs { get; set; } = new List<TblDmcustomerPhanLoaiKh>();
}
