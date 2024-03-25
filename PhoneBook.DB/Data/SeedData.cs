using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.DB.Data
{
    public static class SeedData
    {
        public static async Task EnsureSeedData(IServiceProvider provider)
        {
            var roleMgr = provider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var roleName in RoleNames.AllRoles)
            {
                var role = roleMgr.FindByNameAsync(roleName).Result;

                if (role == null)
                {
                    var result = roleMgr.CreateAsync(new IdentityRole { Name = roleName }).Result;
                    if (result.Succeeded) throw new Exception(result.Errors.First().Description);
                }
            }

            var userMgr = provider.GetRequiredService<UserManager<IdentityUser>>();

            var adminResult = await userMgr.CreateAsync(DefaultUsers.Administrator, "TestAdmin123!");
            var userResult = await userMgr.CreateAsync(DefaultUsers.User, "TestUser123!");

            if (adminResult.Succeeded || userResult.Succeeded)
            {
                var adminUser = await userMgr.FindByEmailAsync(DefaultUsers.Administrator.Email);
                var userUser = await userMgr.FindByEmailAsync(DefaultUsers.User.Email);

                await userMgr.AddToRoleAsync(adminUser, RoleNames.Administrator);
                await userMgr.AddToRoleAsync(userUser, RoleNames.User);
            }
        }
    }
}

