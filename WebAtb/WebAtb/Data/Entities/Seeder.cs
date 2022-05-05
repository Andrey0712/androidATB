using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAtb.Constants;
using WebAtb.Data.Entities.Identity;

namespace WebAtb.Data.Entities
{
    public static class Seeder
    {
        public static void SeederData(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    logger.LogInformation("Seeding Databases");//в консоль сообщает что сидится DB
                    var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();//создаем контекст и дальще накатіваем миграцию
                    context.Database.Migrate();

                    SeedRole(services);//сидим роли
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


        private static void SeedRole(IServiceProvider service)
        {
            var roleManeger = service.GetRequiredService<RoleManager<AppRole>>();
            var userManeger = service.GetRequiredService<UserManager<AppUser>>();

            if (!roleManeger.Roles.Any())
            {
                var rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;
                rez = roleManeger.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
            }

            if (!userManeger.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    FirstName = "Петро",
                    SecondName = "Мельник",
                    Phone = "+38098839384",
                    Photo = "dptnse0x.q12.jpeg"
                };
                var result = userManeger.CreateAsync(user, "12345").Result;
                if (result.Succeeded)
                {
                    result = userManeger.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }

        }
    }
}
