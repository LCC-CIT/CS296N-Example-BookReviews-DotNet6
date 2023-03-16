using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> signInManager;
    private readonly UserManager<AppUser> userManager;

    public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
    {
        userManager = userMngr;
        signInManager = signInMngr;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Username };
            // Temporary assignment of user's real name (screen name?)
            user.Name = user.UserName; // TODO: Add a field to the registration form for real name
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult LogIn(string returnURL = "")
    {
        var model = new LoginVM { ReturnUrl = returnURL };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result =
                await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("", "Invalid username/password.");
        return View(model);
    }
}