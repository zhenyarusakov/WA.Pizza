using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Core.Entities.SmsSender;
using WA.Pizza.Infrastructure.Abstractions.SenderInterface;

namespace WA.Pizza.Api.Controllers;

public class SmsSenderController: BaseApiController
{
    private readonly ISmsSenderService _senderService;
    
    public SmsSenderController(ISmsSenderService senderService)
    {
        _senderService = senderService;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendSms(SmsMessage message)
    {
        await _senderService.SendSmsAsync(message);
    
        return Ok("Success");
    }
}