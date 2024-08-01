using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly GmailSettings _gmailSettings;
        private readonly IConfiguration _configuration;
        public string MailerSendHost { get; set; }
        public string MailerSendPort { get; set; }
        public string MailerSendUsername { get; set; }
        public string MailerSendPassword { get; set; }

        public EmailSender(IOptions<GmailSettings> gmailSettings, IConfiguration configuration)
        {
            _gmailSettings = gmailSettings.Value;
            _configuration = configuration;
            MailerSendHost = _configuration.GetValue<string>("mailersend:host");
            MailerSendPort = _configuration.GetValue<string>("mailersend:port");
            MailerSendUsername = _configuration.GetValue<string>("mailersend:username");
            MailerSendPassword = _configuration.GetValue<string>("mailersend:password");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                string host = MailerSendHost;
                int.TryParse(MailerSendPort, out int port);
                string username = MailerSendUsername;
                string password = MailerSendPassword;

                var smtpClient = new System.Net.Mail.SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username, "SagaciousTrove"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);
                smtpClient.SendMailAsync(mailMessage);

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}

