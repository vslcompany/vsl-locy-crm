namespace vsl_crm_api.Models.Requests
{
    public class CreateDepartmentRequest
    {
        public required string NameVI { get; set; }
        public string? NameEN { get; set; }
        public required long IdVanPhong { get; set; }
    }
}
