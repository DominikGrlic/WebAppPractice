namespace SimpleWebApp.Repositories;

public static class EmailHelper
{
    private const string confirmFileName = "email-confirm.html";
    private const string passReset = "pass-reset.html";

    public static async Task<string> GenerateConfirmEmail(string userName, string link)
    {
        var content = await File.ReadAllTextAsync(confirmFileName);

        return content
            .Replace("{{USERNAME}}", userName)
            .Replace("{{LINK}}", link)
            .Replace("{{CURRENT_YEAR}}", DateTime.UtcNow.Year.ToString());
    }

    public static async Task<string> GeneratePasswordReset(string userName, string link)
    {
        var content = await File.ReadAllTextAsync(passReset);

        return content
            .Replace("{{USERNAME}}", userName)
            .Replace("{{RESET_LINK}}", link)
            .Replace("{{CURRENT_YEAR}}", DateTime.UtcNow.Year.ToString());
    }
}