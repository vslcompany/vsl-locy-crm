using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcustomerPhanLoaiKh
{
    public int Id { get; set; }

    public long? Iddmcustomer { get; set; }

    public long? IddmphanLoaiKhachHang { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmphanLoaiKhachHang? IddmphanLoaiKhachHangNavigation { get; set; }
}
