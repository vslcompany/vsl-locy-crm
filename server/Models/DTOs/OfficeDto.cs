namespace vsl_crm_api.Models.DTOs
{
    public class OfficeDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public long? IdCountry { get; set; }
        public string? QuocGia { get; set; }
        public string? ThanhPho { get; set; }
        public long? IdCity { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? AddressVI { get; set; }
        public string? AddressEN { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
        public string? TaxCode { get; set; }
        public bool? FlagFavorite { get; set; }
    }
}
