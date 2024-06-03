using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysUserNotification
{
    public long Id { get; set; }

    public long? Iduser { get; set; }

    public bool? FlagNewJob { get; set; }

    public bool? FlagDebit { get; set; }

    public virtual TblSysUser? IduserNavigation { get; set; }
}
