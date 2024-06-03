namespace vsl_crm_api.Models.Requests
{
    public class CheckOTPRequest
    {
        public required string Email { get; set; }
        public required string Otp { get; set; }
    }
}
