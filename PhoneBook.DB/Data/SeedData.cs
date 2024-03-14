using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if(role == null)
                {
                    var result = roleMgr.CreateAsync(new IdentityRole { Name = roleName }).Result;
                    if (result.Succeeded) throw new Exception(result.Errors.First().Description);
                }
            }

            var userMgr = provider.GetRequiredService<UserManager<IdentityUser>>();

            var adminResult = await userMgr.CreateAsync(DefaultUsers.Administrator, "TestAdmin123!");
            var userResult = await userMgr.CreateAsync(DefaultUsers.User, "TestUser123!");
            
            if(adminResult.Succeeded || userResult.Succeeded)
            {
                var adminUser = await userMgr.FindByEmailAsync(DefaultUsers.Administrator.Email);
                var userUser = await userMgr.FindByEmailAsync(DefaultUsers.User.Email);

                await userMgr.AddToRoleAsync(adminUser, RoleNames.Administrator);
                await userMgr.AddToRoleAsync(userUser, RoleNames.User);
            }
        }
    }

    public static class RoleNames
    {
        public const string Administrator = "Администратор";
        public const string User = "Пользователь";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Administrator;
                yield return User;
            }
        }
    }

    public static class DefaultUsers
    {
        public static readonly IdentityUser Administrator = new IdentityUser
        {
            Email = "Admin@mail.ru",
            EmailConfirmed = true,
            UserName = "Admin@mail.ru"
        };
        public static readonly IdentityUser User = new IdentityUser
        {
            Email = "Admin@mail.ru",
            EmailConfirmed = true,
            UserName = "Admin@mail.ru"
        };
    }
}

