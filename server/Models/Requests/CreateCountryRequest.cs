namespace vsl_crm_api.Models.Requests
{
    public class CreateCountryRequest
    {
        public required string Code { get; set; }
        public required string NameVI { get; set; }
        public string? NameEN { get; set; }
    }
}
