using System.Threading.Tasks;
using WA.Pizza.Infrastructure.DTO.MailSender;

namespace WA.Pizza.Infrastructure.Abstractions.SenderInterface;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
    Task SendWelcomeEmailAsync(string ToEmail, string UserName);
}