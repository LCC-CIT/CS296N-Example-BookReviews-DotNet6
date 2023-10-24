using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Controllers;

// [Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<AppUser> userManager;

    public UserController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr)
    {
        userManager = userMngr;
        roleManager = roleMngr;
    }

    public async Task<IActionResult> Index()
    {
        var users = new List<AppUser>();
        users = await userManager.Users.ToListAsync();
        foreach (var user in users) user.RoleNames = await userManager.GetRolesAsync(user);
        var model = new UserVM
        {
            Users = users,
            Roles = roleManager.Roles
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // if failed
                var errorMessage = "";
                foreach (var error in result.Errors) errorMessage += error.Description + " | ";
                TempData["message"] = errorMessage;
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddToAdmin(string id)
    {
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it.";
        }
        else
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, adminRole.Name);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromAdmin(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        await userManager.RemoveFromRoleAsync(user, "Admin");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        await roleManager.DeleteAsync(role);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdminRole()
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
        return RedirectToAction("Index");
    }

    // the Add() methods work like the Register() methods in AccountController

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Username };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }
}