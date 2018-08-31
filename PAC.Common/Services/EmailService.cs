using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PAC.Common.Services
{
    /// <summary>
    /// This is a builtin mail from .NET
    /// </summary>
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            if (_emailSettings.Enabled)
            {
                using (var client = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort))
                {
                    if (_emailSettings.UseSSL)
                        client.EnableSsl = true;

                    if (!string.IsNullOrEmpty(_emailSettings.Username))
                        client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                    client.Send(new MailMessage(_emailSettings.FromEmail, emailTo, subject, message));
                }
            }
            return Task.CompletedTask;
        }
    }
}
