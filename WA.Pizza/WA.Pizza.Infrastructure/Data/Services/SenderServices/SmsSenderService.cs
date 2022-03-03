using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WA.Pizza.Core.Entities.SmsSender;
using WA.Pizza.Infrastructure.Abstractions.SenderInterface;

namespace WA.Pizza.Infrastructure.Data.Services.SenderServices;

public class SmsSenderService: ISmsSenderService
{
    private readonly ITwilioRestClient _client;

    public SmsSenderService(ITwilioRestClient client)
    {
        _client = client;
    }

    public async Task SendSmsAsync(SmsMessage message)
    {
        await MessageResource.CreateAsync(
            to: new PhoneNumber(message.To),
            from: new PhoneNumber(message.From),
            body: message.Message,
            client: _client
        );
    }
}