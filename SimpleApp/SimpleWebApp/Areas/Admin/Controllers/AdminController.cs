using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;

namespace SimpleWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    // TODO: make authorized admin panel showing list of users!
    private readonly UserManager<AppUser> _userManager;

    public AdminController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
        var userViewModels = new List<UserViewModel>();
        var users = _userManager.Users.ToList();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userViewModels.Add(new UserViewModel
            {
                User = user,
                Roles = roles
            });
        }

        return View(userViewModels);
    }
}