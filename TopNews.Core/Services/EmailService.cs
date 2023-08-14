using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public async Task SendEmail(string toEmail, string subject, string body)
        {
            string password = _configuration["EmailSetting:Password"];
            string SMTP = _configuration["EmailSetting:SMTP"];
            string fromEmail = _configuration["EmailSetting:User"];
            int port = Int32.Parse(_configuration["EmailSetting:PORT"]);

            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.Body = bodyBuilder.ToMessageBody();

            using(SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect(SMTP, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(fromEmail, toEmail);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }
}
