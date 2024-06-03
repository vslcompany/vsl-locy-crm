using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblNotification
{
    public long Id { get; set; }

    public Guid? GuidId { get; set; }

    public long? IdNguoiGui { get; set; }

    public long? IdDoiTuongLienQuan { get; set; }

    public string? KieuDoiTuongLienQuan { get; set; }

    public long? IdNguoiNhan { get; set; }

    public bool? TrangThaiGui { get; set; }

    public bool? TrangThaiNhan { get; set; }

    public DateTime? Cd { get; set; }

    public DateTime? Ud { get; set; }

    public long? IdLoaiNotification { get; set; }

    public string? ListDoiTuongLienQuan { get; set; }
}
