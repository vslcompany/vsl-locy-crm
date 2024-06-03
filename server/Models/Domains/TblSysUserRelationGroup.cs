using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysUserRelationGroup
{
    public long Id { get; set; }

    public long? Idgroup { get; set; }

    public long? Iduser { get; set; }

    public virtual TblSysUserGroup? IdgroupNavigation { get; set; }

    public virtual TblSysUser? IduserNavigation { get; set; }
}
