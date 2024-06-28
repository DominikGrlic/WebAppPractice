using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;
using SimpleWebApp.Models.Forms;

namespace SimpleWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    // TODO: make authorized admin panel showing list of users!
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _appDbContext;
    private readonly SignInManager<AppUser> _signInManager;

    public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = context;
        _signInManager = signInManager;
    }


    public async Task<IActionResult> Index(string? srchTab, int roleTab = 0)
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

        ViewBag.SrchTab = srchTab;

        var roles = await _roleManager.Roles.ToListAsync();

        ViewBag.Roles = new SelectList(roles, "Name", "Name");

        return View(model);
    }

    //GET: Admin/Edit/5
    public async Task<IActionResult> Update(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            RedirectToAction(nameof(Index));
        }

        var roles = await _roleManager.Roles.ToListAsync();
        var userRoles = await _userManager.GetRolesAsync(user!);

        ViewBag.Roles = new SelectList(roles, "Name", "Name", userRoles.First());

        var model = new EditUserForm()
        {
            Id = user!.Id,
            Email = user.Email!,
        };

        return user is null ? RedirectToAction(nameof(Index)) : View(model);
    }

    //POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EditUserForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var user = await _userManager.FindByIdAsync(form.Id!);

        if (user == null)
        {
            // RARE ERROR
            return RedirectToAction(nameof(Index));
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        if (user.Email != form.Email)
        {
            user.UserName = form.Email;
            user.Email = form.Email;

            await _userManager.UpdateAsync(user);
        }

        await _userManager.RemoveFromRolesAsync(user, userRoles);

        await _userManager.AddToRoleAsync(user, form.Role);

        //await _signInManager.RefreshSignInAsync(user);

        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewUserInputModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser appUser = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            await _appDbContext.SaveChangesAsync();

            if (result.Succeeded)
            {
                var selectedRole = await _roleManager.FindByNameAsync(model.Role);

                if (selectedRole is null)
                {
                    //TODO: IF ROLE DONT EXIST (RARE ERROR) SHOW ERROR
                }

                var resp = await _userManager.AddToRoleAsync(appUser, selectedRole!.Name!);

                if (!resp.Succeeded)
                {
                    //TODO: SHOW ERRORS
                }

                return RedirectToAction(nameof(Index));
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
        return RedirectToAction(nameof(Index));
    }


}