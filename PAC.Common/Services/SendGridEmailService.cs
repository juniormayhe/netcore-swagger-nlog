using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PAC.Common.Services;
using System;
using System.Threading.Tasks;

namespace PAC.Common.Extensions
{
    public class SendGridEmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        private ILogger<EmailService> _logger;

        public SendGridEmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            if (_emailSettings.Enabled)
            {
                _logger.LogInformation($"##Start## Sending email via SendGrid to: {emailTo} subject: {subject} message: {message}");


                //adding sendgrid causes: Found conflicts between different versions of "Microsoft.AspNetCore.Http.Abstractions" that could not be resolved.These reference conflicts are listed in the build log when log verbosity is set to detailed.PAC.Common

                //var client = new SendGrid.SendGridClient(_emailSettings.RemoteServerAPI);
                //var sendGridMessage = new SendGrid.Helpers.Mail.SendGridMessage { From = new SendGrid.Helpers.Mail.EmailAddress(_emailSettings.Username) };
                //sendGridMessage.AddTo(emailTo);
                //sendGridMessage.Subject = subject;
                //sendGridMessage.HtmlContent = message;
                //client.SendEmailAsync(sendGridMessage);
            }
            return Task.CompletedTask;
        }
    }
}