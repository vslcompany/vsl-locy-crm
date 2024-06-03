using System.Net.Mail;
using System.Net;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Models.Configs;

namespace vsl_crm_api.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailSettings;

        public EmailService(IConfiguration config)
        {
            _emailSettings = new EmailConfig()
            {
                SmtpServer = config["EmailSettings:SmtpServer"] ?? "",
                SmtpPort = int.Parse(config["EmailSettings:SmtpPort"] ?? "0"),
                SenderEmail = config["EmailSettings:SenderEmail"] ?? "",
                SenderName = config["EmailSettings:SenderName"] ?? "",
                SenderPassword = config["EmailSettings:SenderPassword"] ?? "",
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.SmtpPort,
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
