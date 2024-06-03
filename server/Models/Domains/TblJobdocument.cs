using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblJobdocument
{
    public long Id { get; set; }

    public long? Idjob { get; set; }

    public long? IddmtoDo { get; set; }

    public long? IduserUpload { get; set; }

    public long? Idjobtodo { get; set; }

    public int? EnumDocumentType { get; set; }

    public int? EnumReportUse { get; set; }

    public string? Path { get; set; }

    public string? FileName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public long? IdtheoDoiVanChuyen { get; set; }

    public long? Idrequest { get; set; }

    public long? IddieuPhoi { get; set; }

    public int? EnumAttachType { get; set; }

    public bool? FlagCategory { get; set; }

    public long? Idbooking { get; set; }

    public bool? FlagRpt { get; set; }

    public int? EnumTypeOfTransport { get; set; }

    public int? EnumLclfcl { get; set; }

    public int? EnumDocument { get; set; }

    public virtual TblSysUser? IduserUploadNavigation { get; set; }
}
