using BookReviews.Models;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Data
{
    public class SeedUsers
    {
        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<AppUser> _userManager;
        // TODO: add static constructor

        public static async Task CreateUsers(IServiceProvider provider)
        {
            _roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = provider.GetRequiredService<UserManager<AppUser>>();

            const string MEMBER = "Member";
            await CreateRole(MEMBER);
            const string ADMIN = "Admin";
            await CreateRole(ADMIN);

            // TODO: Use user secrets to hide the password
            const string SECRET_PASSWORD = "Secret!123";
            await CreateUser("admin", "", SECRET_PASSWORD, ADMIN);

            // Add some fake users for testing
            await CreateUser("Emma", "Watson", SECRET_PASSWORD, MEMBER);
            await CreateUser("Daniel", "Radcliffe", SECRET_PASSWORD, MEMBER);
            await CreateUser("Brian", "Bird", SECRET_PASSWORD, MEMBER);
        }

        private static async Task CreateRole(string roleName)
        {
            // if role doesn't exist, create it
            if (await _roleManager.FindByNameAsync(roleName) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateUser(string firstName, string lastName, string password, string role)
        {
            // if username doesn't exist, create it and add to role if (await userManager.FindByNameAsync(username) == null) {
            AppUser user = new AppUser { UserName = firstName + lastName,
                Name = firstName + " " + lastName};
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}


