namespace vsl_crm_api.Models.Requests
{
    public class CreatePortRequest
    {
        public long? IdQuocGia { get; set; }
        public long? IdCity { get; set; }
        public required string Code { get; set; }
        public string? TaxCode { get; set; }
        public required string NameVI { get; set; }
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
