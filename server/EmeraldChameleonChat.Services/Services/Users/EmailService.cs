using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;
        private const string Mail = "MailSettings:Mail";
        private const string Password = "MailSettings:Password";
        private const string Host = "MailSettings:Host";
        private const string Port = "MailSettings:Port";

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string to, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration[Mail]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration[Host], int.Parse(_configuration[Port]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration[Mail], _configuration[Password]);
            var result = await smtp.SendAsync(email);
            smtp.Disconnect(true);

            
        }
    }
}
