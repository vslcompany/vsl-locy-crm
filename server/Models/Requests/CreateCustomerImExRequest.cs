namespace vsl_crm_api.Models.Requests
{
    public class CreateCustomerImExRequest
    {
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Vessel { get; set; }
        public string? Term { get; set; }
        public string? Code { get; set; }
        public string? Commd { get; set; }
        public string? Vol { get; set; }
        public string? Unt { get; set; }
        public long? IdUserCreate { get; set; }
        public long? IdFromPort { get; set; }
        public long? IdToPort { get; set; }
        public long? IdFromCountry { get; set; }
        public long? IdToCountry { get; set; }
        public long? IdCustomer { get; set; }
    }
}
