using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblBaoCaoCongViec
{
    public long Id { get; set; }

    public long? IdnhanVien { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? CreateDate { get; set; }

    public long? IduserCreate { get; set; }

    public double? Amount { get; set; }

    public string? XepLoai { get; set; }

    public string? GhiChu { get; set; }

    public long? IdxepLoai { get; set; }

    public DateTime? ThoiGianThucHien { get; set; }

    public virtual TblNhanSu? IdnhanVienNavigation { get; set; }

    public virtual TblSysUser? IduserCreateNavigation { get; set; }

    public virtual TblDmxepLoaiBaoCaoCv? IdxepLoaiNavigation { get; set; }
}
