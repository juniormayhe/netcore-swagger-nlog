using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PAC.Extensions
{
    public static class EmailServiceExtension
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            if (hostingEnvironment.IsDevelopment() || hostingEnvironment.IsStaging())
            {
                services.AddSingleton<IEmailService, EmailService>();
            }
            else
            {
                //configure email service for production, sendgrid or create a class for a another transactional email service https://www.ventureharbour.com/transactional-email-service-best-mandrill-vs-sendgrid-vs-mailjet/
                services.AddSingleton<IEmailService, SendGridEmailService>();
            }
            return services;
        }
    }
}
