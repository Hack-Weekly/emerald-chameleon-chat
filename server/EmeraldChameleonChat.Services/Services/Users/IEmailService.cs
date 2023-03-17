using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Services.Users
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string to, string body);
    }
}