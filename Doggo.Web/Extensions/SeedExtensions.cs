namespace Doggo.Api.Extensions;

using Domain.Constants;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

public class Some
{
    public int Age { get; set; }

    public string Name { get; set; }
}

public static class SeedExtensions
{
    public static async Task SeedAllData(this IApplicationBuilder applicationBuilder)
    {
        List<User> users = new List<User>();
        string allText = System.IO.File.ReadAllText("/Users/turchynovychnazarii/RiderProjects/Doggo/Doggo.Web/Extensions/users.json");

        var a = JsonConvert.DeserializeObject<Some[]>(allText);

        Console.WriteLine(a);
    }

    public static async Task SeedUsersAndRolesAsync(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if (!await roleManager.RoleExistsAsync(RoleConstants.Admin))
                    await roleManager.CreateAsync(new Role(RoleConstants.Admin));
                if (!await roleManager.RoleExistsAsync(RoleConstants.User))
                    await roleManager.CreateAsync(new Role(RoleConstants.User));
                if (!await roleManager.RoleExistsAsync(RoleConstants.Walker))
                    await roleManager.CreateAsync(new Role(RoleConstants.Walker));
                if (!await roleManager.RoleExistsAsync(RoleConstants.DogOwner))
                    await roleManager.CreateAsync(new Role(RoleConstants.DogOwner));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new User
                    {
                        FirstName = "admin",
                        LastName = "admin",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "qwe123");
                    await userManager.AddToRoleAsync(newAdminUser, RoleConstants.Admin);
                }


                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User
                    {
                        FirstName = "user",
                        LastName = "user",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "qwe123");
                    await userManager.AddToRoleAsync(newAppUser, RoleConstants.User);
                }
            }
        }
}