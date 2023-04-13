using BookReviews.Models;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Data;

public class SeedUsers
{
    private static RoleManager<IdentityRole> roleManager;

    private static UserManager<AppUser> userManager;
    // TODO: add static constructor

    public static async Task CreateUsers(IServiceProvider provider)
    {
        roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        userManager = provider.GetRequiredService<UserManager<AppUser>>();

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
        if (await roleManager.FindByNameAsync(roleName) == null)
            await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    private static async Task CreateUser(string firstName, string lastName, string password, string role)
    {
        // if username doesn't exist, create it and add to role if (await userManager.FindByNameAsync(username) == null) {
        var user = new AppUser
        {
            UserName = firstName + lastName,
            Name = firstName + " " + lastName
        };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded) await userManager.AddToRoleAsync(user, role);
    }
}