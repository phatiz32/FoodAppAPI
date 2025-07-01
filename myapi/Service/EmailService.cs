
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Service
{
    public class EmailService : IEmailServices
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task<bool> SendMailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.SenderEmail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                var builder = new BodyBuilder { HtmlBody = body };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                //connect server smtp
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                //login smtp server
                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
                await smtp.SendAsync(email);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}