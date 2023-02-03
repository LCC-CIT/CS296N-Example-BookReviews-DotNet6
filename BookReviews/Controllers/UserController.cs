using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookReviews.Models;
using System.Data;

namespace BookReviews.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager; 
        private RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }
        public IActionResult Index()
        {
            List<AppUser> users = new List<AppUser>();
            users = userManager.Users.ToList();
            foreach(AppUser user in users)
            //foreach (AppUser user in userManager.Users)
            //AppUser user = userManager.FindByNameAsync("admin").Result;
            {
                // user.RoleNames = await userManager.GetRolesAsync(user);
                var task = userManager.GetRolesAsync(user);
                task.Wait();
                user.RoleNames = task.Result;
                // users.Add(user);
            }
            UserVM model = new UserVM
            {
                Users = users,
                Roles = roleManager.Roles
            }; return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id); 
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user); 
                if (!result.Succeeded)
                { // if failed
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        // the Add() methods work like the Register() methods from 16-11 and 16-12
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin"); 
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id); 
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id); 
            await userManager.RemoveFromRoleAsync(user, "Admin"); 
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id); 
            await roleManager.DeleteAsync(role); 
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin")); 
            return RedirectToAction("Index");
        }
    }
}
