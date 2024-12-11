using System.Net;
using System.Net.Mail;

namespace AutoNaJuz.Services.ConcreteServices
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private const string DefaultSenderName = "Auto na Już";

        public EmailService(string host, int port, bool enableSsl, string? username, string password)
        {
            _smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(username, password)
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                string fromEmail = (_smtpClient.Credentials as NetworkCredential)?.UserName ?? "default@example.com";
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, DefaultSenderName), // Stała nazwa nadawcy
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw new InvalidOperationException("Wystąpił błąd podczas wysyłania e-maila.", ex);
            }
        }
    }
}