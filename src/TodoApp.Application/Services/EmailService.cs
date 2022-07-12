using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Hangfire.Models;

namespace TodoApp.Application.Services
{
    internal class EmailService : IEmailService
    {
        protected readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void Send(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.Username));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
