using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcustomerTuyenHang
{
    public int Id { get; set; }

    public int? EnumLoaiVanChuyen { get; set; }

    public long? IdquocGiaDi { get; set; }

    public long? IdquocGiaDen { get; set; }

    public long? IdcangDi { get; set; }

    public long? IdcangDen { get; set; }

    public long? Iddmcustomer { get; set; }

    public long? IddmloaiHinhVanChuyen { get; set; }

    public virtual TblDmport? IdcangDenNavigation { get; set; }

    public virtual TblDmport? IdcangDiNavigation { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmloaiHinhVanChuyen? IddmloaiHinhVanChuyenNavigation { get; set; }

    public virtual TblDmcountry? IdquocGiaDenNavigation { get; set; }

    public virtual TblDmcountry? IdquocGiaDiNavigation { get; set; }
}
