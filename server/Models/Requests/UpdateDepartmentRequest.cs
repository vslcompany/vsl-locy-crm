namespace vsl_crm_api.Models.Requests
{
    public class UpdateDepartmentRequest
    {
        public long Id { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public long? IdVanPhong { get; set; }
    }
}
