namespace vsl_crm_api.Models.Requests
{
    public class ChangePasswordRequest
    {
        public long Id { get; set; }
        public required string Password { get; set; }
        public required string NewPassword { get; set; }
    }
}
