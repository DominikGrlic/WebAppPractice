using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models.Forms;

public class EditUserForm
{
    public string Id { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;
}