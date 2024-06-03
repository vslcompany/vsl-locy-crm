using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysUserGroup
{
    public long Id { get; set; }

    public string? GroupName { get; set; }

    public string? GhiChu { get; set; }

    public string? Permission { get; set; }

    public virtual ICollection<TblJobuserAccess> TblJobuserAccesses { get; set; } = new List<TblJobuserAccess>();

    public virtual ICollection<TblSysUserRelationGroup> TblSysUserRelationGroups { get; set; } = new List<TblSysUserRelationGroup>();

    public virtual ICollection<TblSysUser> TblSysUsers { get; set; } = new List<TblSysUser>();
}
