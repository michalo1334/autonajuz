using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutoNaJuz.Web
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(string host, int port, bool enableSsl, string username, string password)
        {
            _smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(username, password)
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress((_smtpClient.Credentials as NetworkCredential)?.UserName ?? "default@example.com"), // Domyślna wartość, jeśli rzutowanie nie powiedzie się
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
