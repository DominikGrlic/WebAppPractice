using SimpleWebApp.Models;

namespace SimpleWebApp.Interfaces;

public interface IEmailConfirmSender
{
    Task<BaseResponse> SendEmailConfirmAsync(string userEmail, string link);
    Task<bool> SendEmailPassResetAsync(string email, string link);
}