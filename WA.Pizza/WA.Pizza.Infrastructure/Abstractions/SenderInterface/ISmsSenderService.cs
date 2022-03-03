using System.Threading.Tasks;
using WA.Pizza.Core.Entities.SmsSender;

namespace WA.Pizza.Infrastructure.Abstractions.SenderInterface;

public interface ISmsSenderService
{
    Task SendSmsAsync(SmsMessage message);
}