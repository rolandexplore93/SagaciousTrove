using System;
using System.Xml.Linq;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Utility
{
	public class EmailSender : IEmailSender
    {
        private readonly GmailSettings _gmailSettings;
        public string SendGridSecret { get; set; }

        public EmailSender(IOptions<GmailSettings> gmailSettings, IConfiguration _config)
        {
            _gmailSettings = gmailSettings.Value;
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendGridSecret);
            var from = new EmailAddress("roland2rule@gmail.com", "Sagacious Trove");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            return client.SendEmailAsync(msg);

            //string pw = _gmailSettings.pw;
            //string name = _gmailSettings.name;

            //var emailToSend = new MimeMessage();
            //emailToSend.From.Add(MailboxAddress.Parse("hello@sagacious.com"));
            //emailToSend.To.Add(MailboxAddress.Parse(email));
            //emailToSend.Subject = subject;
            //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //using (var emailClient = new SmtpClient())
            //{
            //    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    emailClient.Authenticate(name, pw);
            //    emailClient.Send(emailToSend);
            //    emailClient.Disconnect(true);
            //}
            //return Task.CompletedTask;
        }
    }
}

