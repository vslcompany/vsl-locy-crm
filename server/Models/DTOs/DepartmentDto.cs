namespace vsl_crm_api.Models.DTOs
{
    public class DepartmentDto
    {
        public long Id { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? GhiChu { get; set; }
        public bool? FlagFavorite { get; set; }
        public long IdVanPhong { get; set; }
    }
}
