using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WA.Pizza.Infrastructure.DTO.MailSender;

public class MailRequest
{
    public string? ToEmail { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<IFormFile> Attachments { get; set; } = new ();
}