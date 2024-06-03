namespace vsl_crm_api.Models.Requests
{
    public class UpdateProfileRequest
    {
        public long id { get; set; }
        public string? hoTenVI { get; set; }
        public string? namsinh { get; set; }
        public int? gioitinh { get; set; }
        public string? quequan { get; set; }
        public string? diachi { get; set; }
        public string? soCMT { get; set; }
        public string? noiCapCMT { get; set; }
        public string? ngayCapCMT { get; set; }
        public string? didong { get; set; }
        public string? email { get; set; }
        public string? photoURL { get; set; }
    }
}
