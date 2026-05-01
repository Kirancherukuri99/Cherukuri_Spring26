using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cherukuri_Spring26
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager =
                services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminAlreadyExists = await roleManager.RoleExistsAsync(Constants.Admin);
            if (adminAlreadyExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole(Constants.Admin));

            var ownerAlreadyExists = await roleManager.RoleExistsAsync(Constants.Owner);
            if (ownerAlreadyExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole(Constants.Owner));

            var tenantAlreadyExists = await roleManager.RoleExistsAsync(Constants.Tenant);
            if (tenantAlreadyExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole(Constants.Tenant));
        }

        public static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@property.com")
                .SingleOrDefaultAsync();

            if (testAdmin != null)
            {
                return;
            }

            testAdmin = new IdentityUser
            {
                UserName = "admin@property.com",
                Email = "admin@property.com"
            };

            await userManager.CreateAsync(testAdmin, "Admin@1234");
            await userManager.AddToRoleAsync(testAdmin, Constants.Admin);
        }
    }
}
