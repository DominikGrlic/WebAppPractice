using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SimpleWebApp.Interfaces;
using SimpleWebApp.Models;

namespace SimpleWebApp.Repositories;

public class EmailConfirmSender : IEmailConfirmSender
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailConfirmSender(IOptions<EmailConfiguration> config)
    {
        _emailConfiguration = config.Value;
    }

    public async Task<BaseResponse> SendEmailConfirmAsync(string userEmail, string link)
    {
        try
        {
            var mimeMessage = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_emailConfiguration.Email),
                From = { new MailboxAddress(Encoding.UTF8, _emailConfiguration.DisplayName, _emailConfiguration.Email) },
                Date = DateTimeOffset.UtcNow,
                Priority = MessagePriority.Urgent,
                To = { MailboxAddress.Parse(userEmail) },
                Subject = "Confirm you email address!"
            };


            var multipart = new Multipart("mixed");

            var htmlContent = await EmailHelper.GenerateConfirmEmail(userEmail, link);

            var body = new TextPart(TextFormat.Html)
            {
                Text = htmlContent
            };

            multipart.Add(body);
            mimeMessage.Body = multipart;

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port);
            await client.AuthenticateAsync(_emailConfiguration.Email, _emailConfiguration.Password);

            var response = await client.SendAsync(mimeMessage);

            //await client.DisconnectAsync(true);

            return new BaseResponse()
            {
                IsError = !response.Contains("Ok"),
                Message = response
            };
        }
        catch (Exception e)
        {
            return new BaseResponse()
            {
                IsError = true,
                Exception = e,
                Message = "An error has occured!"
            };
        }
    }

    public async Task<bool> SendEmailPassResetAsync(string userEmail, string link)
    {
        try
        {
            var mimeMessage = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_emailConfiguration.Email),
                From = { new MailboxAddress(Encoding.UTF8, _emailConfiguration.DisplayName, _emailConfiguration.Email) },
                Priority = MessagePriority.Urgent,
                Date = DateTimeOffset.UtcNow,
                Subject = "Password reset",
                To = { MailboxAddress.Parse(userEmail) }
            };

            var multipart = new Multipart("mixed");

            var htmlContent = await EmailHelper.GeneratePasswordReset(userEmail, link);

            var body = new TextPart(TextFormat.Html)
            {
                Text = htmlContent
            };

            multipart.Add(body);
            mimeMessage.Body = multipart;

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback += (sender, x, y, z) =>
            {
                return true;
            };

            await client.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port);
            await client.AuthenticateAsync(_emailConfiguration.Email, _emailConfiguration.Password);

            var response = await client.SendAsync(mimeMessage);

            Console.WriteLine(response);

            await client.DisconnectAsync(true);
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}