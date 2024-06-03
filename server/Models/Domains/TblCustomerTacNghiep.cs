using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblCustomerTacNghiep
{
    public long Id { get; set; }

    public long? IdloaiTacNghiep { get; set; }

    public long? IdcustomerList { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? DateCreate { get; set; }

    public long? IduserCreate { get; set; }

    public long? Iddmcustomer { get; set; }

    public DateTime? ThoiGianThucHien { get; set; }

    public long? IdnguoiLienHe { get; set; }

    public string? KhachHangPhanHoi { get; set; }

    public DateTime? NgayPhanHoi { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmloaiTacNghiep? IdloaiTacNghiepNavigation { get; set; }

    public virtual TblDmcontactList? IdnguoiLienHeNavigation { get; set; }

    public virtual TblSysUser? IduserCreateNavigation { get; set; }
}
