using System.Threading.Tasks;

namespace PAC.Common.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }
}