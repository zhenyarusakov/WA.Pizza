using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WA.Pizza.Infrastructure.Abstractions.SenderInterface;
using WA.Pizza.Infrastructure.DTO.MailSender;

namespace WA.Pizza.Api.Controllers;

public class MailController: BaseApiController
{
    private readonly IMailService _mailService;

    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("MailSend")]
    public async Task<IActionResult> SendEmail([FromForm] MailRequest mailRequest)
    {
        await _mailService.SendEmailAsync(mailRequest);

        return Ok();
    }

    [HttpPost("SendWelcomeEmail")]
    public async Task<IActionResult> SendWelcomeEmail([FromForm] string ToEmail, string UserName)
    {
        await _mailService.SendWelcomeEmailAsync(ToEmail, UserName);

        return Ok();
    }
}