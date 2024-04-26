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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _appDbContext;

    public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = context;
    }


    public IActionResult Index(string? srchTab, int roleTab = 0)
    {
        var model = _userManager.Users.ToList().Select(user =>
        {
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            return new UserViewModel { Roles = roles, User = user };

        }).ToList();

        if (!string.IsNullOrEmpty(srchTab))
        {
            model = model.Where(a => a.User.UserName.ToLower().Contains(srchTab.ToLower())).ToList();
        }

        switch (roleTab)
        {
            case 1: model = model.Where(a => a.Roles.Contains("Admin")).ToList(); break;
            case 2: model = model.Where(a => a.Roles.Contains("User")).ToList(); break;
            case 3: model = model.OrderBy(a => a.User.UserName).ToList(); break;
            case 4: model = model.OrderByDescending(a => a.User.UserName).ToList(); break;
        }

        return View(model);
    }

    //GET: Admin/Edit/5
    public async Task<IActionResult> Update(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(user);
        }
    }

    //POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(string id, int roles)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            if (roles != 0)
            {
                if (roles == 1)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    await _userManager.AddToRoleAsync(user, "User");
                }

                else if (roles == 2)
                {
                    await _userManager.RemoveFromRoleAsync(user, "User");
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                else
                {
                    ModelState.AddModelError("", "User role can't be recognised");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user role");
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        else
        {
            ModelState.AddModelError("", "User not found!");
        }

        return View(user);

    }

    // GET: Admin/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserViewModel user)
    {
        if (ModelState.IsValid)
        {
            AppUser appUser = new AppUser
            {
                UserName = user.Email,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(appUser, user.Password);
            await _appDbContext.SaveChangesAsync();

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        //return PartialView("_CreateUserModal", user);
        return View("Index");
    }

}