using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblDmcustomerDanhGium
{
    public long Id { get; set; }

    public long? Iddmcustomer { get; set; }

    public long? IddmcustomerType { get; set; }

    public long? IduserCreate { get; set; }

    public DateTime? DateCreate { get; set; }

    public string? GhiChu { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmcustomerType? IddmcustomerTypeNavigation { get; set; }

    public virtual TblSysUser? IduserCreateNavigation { get; set; }
}
