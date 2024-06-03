using System;
using System.Collections.Generic;

namespace vsl_crm_api.Models.Domains;

public partial class TblSysFcmtoken
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? Fcmtoken { get; set; }

    public virtual TblNhanSu? User { get; set; }
}
