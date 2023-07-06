using FIrstProductCRUD.Constants;
using FIrstProductCRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq.Expressions;

namespace FIrstProductCRUD.Data.RolesIdentity
{
    public class AppDbInitializer
    {

        async public Task CreateSuperUser(UserManager<WebSiteUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var user = await userManager.FindByEmailAsync("SuperUser@gmail.com");

            if (user == null)
            {
                var roleAdmin = new IdentityRole<int> { Name =  RoleNameConstants.Admin};
                var roleUser = new IdentityRole<int> { Name = RoleNameConstants.User };
                var roleSuperUser = new IdentityRole<int> { Name = RoleNameConstants.SuperUser };

                await roleManager.CreateAsync(roleUser);
                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleSuperUser);

                var userName = "SuperUser@gmail.com";
                var password = "12345__ABc";

                var superUser = new WebSiteUser();
                superUser.UserName = userName;
                superUser.Email = userName;

                var result = await userManager.CreateAsync(superUser, password);
              
                if (result.Succeeded)
                {                
                    await userManager.AddToRoleAsync(superUser, roleUser.Name);
                    await userManager.AddToRoleAsync(superUser, roleAdmin.Name);
                    await userManager.AddToRoleAsync(superUser, roleSuperUser.Name);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine("Ошибка создания пользователя: " + error.Description);
                    }
                }

            }
        }
    }
}
