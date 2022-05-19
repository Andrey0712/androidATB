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
                    SeedCateory(services);
                    SeedProduct(services);
                    SeedUserProd(services);
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

        private static void SeedCateory(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.AddRange(new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Name="Монети"
                    },
                    new ProductCategory
                    {
                        Name="Марки"
                    }
                });
                context.SaveChanges();
            }
           
        }

        private static void SeedUserProd(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppEFContext>();
            if (!context.UserProduct.Any())
            {
                var user= context.Users.FirstOrDefault();
                var product = context.Products.FirstOrDefault();
                UserProduct userProduct = new UserProduct
                {
                    UserId = user.Id,
                    ProductId = product.Id
                };
                context.UserProduct.Add(userProduct);
                context.SaveChanges();
            }

        }


        private static void SeedProduct(IServiceProvider service)
        {
            
            var context = service.GetRequiredService<AppEFContext>();

            if (!context.Products.Any())
            {
                var productCategory = context.ProductCategories.FirstOrDefault();


                Product product = new Product
                {
                    ProductCategoryId = productCategory.Id,
                    Name = "Дукат",
                    Description = "Золото.",
                    DateCreate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateFinish = DateTime.SpecifyKind(DateTime.Now.AddDays(2), DateTimeKind.Utc),
                    Price = 500,
                    StartPhoto = "https://monety.com.ua/images/thumbs/0013855_zolota-moneta-1-dukat-gold-1-ducat-349gramm_550.jpeg",
                    ProductImages = new List<ProductImage>
                    {
                        
                        new ProductImage
                        {
                            ProductId= 1,
                            Name ="https://skupkamonet.com.ua/modules/ajaxzoom/axZm/zoomLoad.php?azImg=/img/p/3/8/3/8/3838.jpg&qual=80&width=1200&height=1200"
                        },
                        new ProductImage
                        {
                            ProductId=1,
                            Name="https://vechi.com.ua/images/stories/fcoin/austria/1-dukat-1915-zoloto-austria.jpg"
                        }
                    }

                };

                context.Products.Add(product);

                context.SaveChanges();

            }
           
        }
    }
}
