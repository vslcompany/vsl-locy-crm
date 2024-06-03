using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblCustomerListImEx
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Type { get; set; }

    public string? Pol { get; set; }

    public string? Pod { get; set; }

    public string? Vessel { get; set; }

    public string? Term { get; set; }

    public string? Code { get; set; }

    public string? Commd { get; set; }

    public string? Vol { get; set; }

    public string? Unt { get; set; }

    public DateTime? CreateDate { get; set; }

    public long? IduserCreate { get; set; }

    public long? IdcustomerList { get; set; }

    public string? CountryPol { get; set; }

    public string? CountryPod { get; set; }

    public long? IdfromPort { get; set; }

    public long? IdtoPort { get; set; }

    public long? IdfromCountry { get; set; }

    public long? IdtoCountry { get; set; }

    public long? IddmdieuKienVc { get; set; }

    public long? Iddmcustomer { get; set; }

    public virtual TblDmcustomer? IddmcustomerNavigation { get; set; }

    public virtual TblDmcountry? IdfromCountryNavigation { get; set; }

    public virtual TblDmport? IdfromPortNavigation { get; set; }

    public virtual TblDmcountry? IdtoCountryNavigation { get; set; }

    public virtual TblDmport? IdtoPortNavigation { get; set; }

    public virtual TblSysUser? IduserCreateNavigation { get; set; }
}
