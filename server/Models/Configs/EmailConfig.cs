namespace vsl_crm_api.Models.Configs
{
    public class EmailConfig
    {
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string SenderEmail { get; set; }
        public required string SenderName { get; set; }
        public required string SenderPassword { get; set; }
    }
}
