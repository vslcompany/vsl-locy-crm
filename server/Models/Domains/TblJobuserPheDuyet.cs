using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblJobuserPheDuyet
{
    public long Id { get; set; }

    public long? IduserPheDuyet { get; set; }

    public long? IdphieuThuChi { get; set; }

    public long? IdjobAcoutingRequest { get; set; }

    public long? IddeNghiTamUng { get; set; }

    public long? Idbooking { get; set; }

    public virtual TblSysUser? IduserPheDuyetNavigation { get; set; }
}
