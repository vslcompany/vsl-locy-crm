namespace vsl_crm_api.Models.Requests
{
    public class CreateOperationalRequest
    {
        public required string Name { get; set; }
        public required int R { get; set; }
        public required int G { get; set; }
        public required int B { get; set; }
        public int? NgayTuTraKhach { get; set; }
    }
}
