using SimpleWebApp.Areas.Identity.Data;

namespace SimpleWebApp.Models;

public class UserViewModel
{
    public AppUser User { get; set; }
    public IList<string> Roles { get; set; }
}
