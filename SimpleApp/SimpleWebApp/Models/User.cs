using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.Models;

public class User
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
