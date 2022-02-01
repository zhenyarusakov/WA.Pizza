using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WA.Pizza.Core.Entities.IdentityModels;

public static class CreateAdminAccount
{
    public static async Task CreateAdminAccountAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));

        var defaultAdmin = new ApplicationUser
        {
            UserName = BaseAuthorization.default_admin,
            Email = BaseAuthorization.default_email,
            EmailConfirmed = true
        };

        if (userManager.Users.All(x => x.Id != defaultAdmin.Id))
        {
            await userManager.CreateAsync(defaultAdmin, BaseAuthorization.default_password);
            await userManager.AddToRoleAsync(defaultAdmin, BaseAuthorization.default_role.ToString());
        }
    }
}