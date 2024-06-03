namespace vsl_crm_api.Models.Requests
{
    public class ResetPasswordRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
