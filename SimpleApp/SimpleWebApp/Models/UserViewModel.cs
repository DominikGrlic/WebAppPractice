using System.ComponentModel.DataAnnotations;
using SimpleWebApp.Areas.Identity.Data;

namespace SimpleWebApp.Models;

public class UserViewModel
{
    public AppUser User { get; set; }
    public IList<string> Roles { get; set; }
    //[Required]
    //public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
