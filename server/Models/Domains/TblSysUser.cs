using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysUser
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public long? IdnhanVien { get; set; }

    public long? IdgroupUser { get; set; }

    public bool? Active { get; set; }

    public string? GhiChu { get; set; }

    public bool? Fixed { get; set; }

    public string? Permission { get; set; }

    public bool? StatusLogin { get; set; }

    public virtual TblSysUserGroup? IdgroupUserNavigation { get; set; }

    public virtual TblNhanSu? IdnhanVienNavigation { get; set; }

    public virtual ICollection<TblBaoCaoCongViec> TblBaoCaoCongViecs { get; set; } = new List<TblBaoCaoCongViec>();

    public virtual ICollection<TblCustomerListImEx> TblCustomerListImExes { get; set; } = new List<TblCustomerListImEx>();

    public virtual ICollection<TblCustomerTacNghiep> TblCustomerTacNghieps { get; set; } = new List<TblCustomerTacNghiep>();

    public virtual ICollection<TblDmcustomerDanhGium> TblDmcustomerDanhGia { get; set; } = new List<TblDmcustomerDanhGium>();

    public virtual ICollection<TblDmcustomer> TblDmcustomerIduserCreateNavigations { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmcustomer> TblDmcustomerIduserDeleteNavigations { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmcustomer> TblDmcustomerIduserGiaoViecNavigations { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblDmcustomer> TblDmcustomerIduserTraKhachNavigations { get; set; } = new List<TblDmcustomer>();

    public virtual ICollection<TblJobdocument> TblJobdocuments { get; set; } = new List<TblJobdocument>();

    public virtual ICollection<TblJobuserAccess> TblJobuserAccesses { get; set; } = new List<TblJobuserAccess>();

    public virtual ICollection<TblJobuserPheDuyet> TblJobuserPheDuyets { get; set; } = new List<TblJobuserPheDuyet>();

    public virtual ICollection<TblSysUserNotification> TblSysUserNotifications { get; set; } = new List<TblSysUserNotification>();

    public virtual ICollection<TblSysUserRelationGroup> TblSysUserRelationGroups { get; set; } = new List<TblSysUserRelationGroup>();
}
