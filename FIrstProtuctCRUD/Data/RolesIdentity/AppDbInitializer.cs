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
                var role1 = new IdentityRole<int> { Name = "admin" };
                var role2 = new IdentityRole<int> { Name = "user" };
                var role3 = new IdentityRole<int> { Name = "superUser" };

                await roleManager.CreateAsync(role1);
                await roleManager.CreateAsync(role2);
                await roleManager.CreateAsync(role3);

                var userName = "SuperUser@gmail.com";
                var password = "12345__ABc";

                var superUser = new WebSiteUser();
                superUser.UserName = userName;
                superUser.Email = userName;

                var result = await userManager.CreateAsync(superUser, password);
                Console.WriteLine(result.Succeeded + "ccccccccccccccccccccccccccccccccccccccccccccccccccc");
                if (result.Succeeded)
                {                
                    await userManager.AddToRoleAsync(superUser, role1.Name);
                    await userManager.AddToRoleAsync(superUser, role2.Name);
                    await userManager.AddToRoleAsync(superUser, role3.Name);
                }
                else
                {
                    // Создание пользователя не удалось
                    // Обработайте ошибки, полученные в результате создания пользователя
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine("Ошибка создания пользователя: " + error.Description);
                    }
                }

            }
        }
    }
}
