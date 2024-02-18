namespace SimpleWebApp.Repositories;

public static class EmailHelper
{
    private const string confirmFileName = "email-confirm.html";

    public static async Task<string> GenerateConfirmEmail(string userName, string link)
    {
        var content = await File.ReadAllTextAsync(confirmFileName);

        return content
            .Replace("{{USERNAME}}", userName)
            .Replace("{{RESET_LINK}}", link)
            .Replace("{{CURRENT_YEAR}}", DateTime.UtcNow.Year.ToString());
    }
}