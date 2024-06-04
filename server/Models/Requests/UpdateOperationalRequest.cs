namespace vsl_crm_api.Models.Requests
{
    public class UpdateOperationalRequest
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? R { get; set; }
        public int? G { get; set; }
        public int? B { get; set; }
        public int? NgayTuTraKhach { get; set; }
    }
}
