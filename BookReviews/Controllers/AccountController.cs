using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookReviews.Models;

namespace BookReviews.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager; 
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
        {
            _userManager = userMngr; _signInManager = signInMngr;
        } 

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            // if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                // Temporary assignment of user's real name (screen name?)
                user.Name = user.UserName; // TODO: Add a field to the registration form for real name
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = "")
        {
            var model = new LoginVm { ReturnUrl = returnUrl }; 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    { return Redirect(model.ReturnUrl); }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password."); 
            return View(model);
        }

    }


}