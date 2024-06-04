namespace vsl_crm_api.Models.DTOs
{
    public class CityDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public long? IdQuocGia { get; set; }
        public string? QuocGia { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? Note { get; set; }
        public bool? FlagFavorite { get; set; }
    }
}
