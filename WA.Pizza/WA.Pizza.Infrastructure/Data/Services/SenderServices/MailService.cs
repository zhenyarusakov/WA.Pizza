﻿using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WA.Pizza.Infrastructure.Abstractions.SenderInterface;
using WA.Pizza.Infrastructure.DTO.MailSender;

namespace WA.Pizza.Infrastructure.Data.Services.SenderServices;

public class MailService: IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;

        var builder = new BodyBuilder();

        byte[] fileBytes;

        foreach (var file in mailRequest.Attachments)
        {
            if (file.Length > 0)
            {
                await using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            }
        }

        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task SendWelcomeEmailAsync(string ToEmail, string UserName)
    {
        string filePath = "D:\\Pizza\\evgeniy-rusakov\\WA.Pizza\\WA.Pizza\\Template\\WelcomeTemplate.html";
        StreamReader streamReader = new StreamReader(filePath);
        string mailText = await streamReader.ReadToEndAsync();
        streamReader.Close();

        mailText = mailText.Replace("[username]", UserName).Replace("[email]", ToEmail);
        
        MimeMessage email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(ToEmail));
        email.Subject = $"Welcome {UserName}";

        BodyBuilder builder = new BodyBuilder();
        builder.HtmlBody = mailText;

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}