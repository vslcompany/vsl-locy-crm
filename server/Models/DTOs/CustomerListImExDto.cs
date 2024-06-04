namespace vsl_crm_api.Models.DTOs
{
    public class CustomerListImExDto
    {
        public long Id { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Vessel { get; set; }
        public string? Term { get; set; }
        public string? Code { get; set; }
        public string? Commd { get; set; }
        public string? Vol { get; set; }
        public string? Unt { get; set; }
        public string? CreateDate { get; set; }
        public long? IdUserCreate { get; set; }
        public string? NguoiTao { get; set; }
        public long? IdFromPort { get; set; }
        public string? CangDi { get; set; }
        public long? IdToPort { get; set; }
        public string? CangDen { get; set; }
        public long? IdFromCountry { get; set; }
        public string? QuocGiaDi { get; set; }
        public long? IdToCountry { get; set; }
        public string? QuocGiaDen { get; set; }
        public long? IdCustomer { get; set; }
    }
}
