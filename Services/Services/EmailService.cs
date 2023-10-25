using System.Net.Mail;
using System.Net;

namespace Services.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("EmailSettings");

            using (var client = new SmtpClient(smtpSettings["SmtpServer"])
            {
                Port = int.Parse(smtpSettings["SmtpPort"]),
                Credentials = new NetworkCredential(smtpSettings["SmtpUsername"], smtpSettings["SmtpPassword"]),
                EnableSsl = true,
            })
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mail.To.Add(to);

                client.Send(mail);
            }
        }
    }
}
