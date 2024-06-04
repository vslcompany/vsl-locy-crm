namespace vsl_crm_api.Models.DTOs
{
    public class PortDto
    {
        public long Id { get; set; }
        public long? IdQuocGia { get; set; }
        public string? QuocGia { get; set; }
        public long? IdCity { get; set; }
        public string? ThanhPho { get; set; }
        public string? Code { get; set; }
        public string? TaxCode { get; set; }
        public string? NameVI { get; set; }
        public string? NameEN { get; set; }
        public string? AddressVI { get; set; }
        public string? AddressEN { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Note { get; set; }
    }
}
