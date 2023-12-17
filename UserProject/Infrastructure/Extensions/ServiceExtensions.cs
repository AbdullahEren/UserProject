using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


namespace UserProject.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("mssqlconnection"),
                                              b => b.MigrationsAssembly("UserProject")));

            
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<RepositoryContext>();
        }

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider, RepositoryContext context)
        {
            UserManager<ApplicationUser> userManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<ApplicationRole> roleManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();

            var rolesToAdd = new string[] { "User", "User", "User", "User", "User", "User", "User", "User", "User", "User" };

            foreach (var role in rolesToAdd)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }

            var usersToAdd = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Name = "Leanne Graham",
                    UserName = "Bret",
                    Email = "Sincere@april.biz",
                    PhoneNumber = "1-770-736-8031 x56442",
                    Website = "hildegard.org",
                    CompanyId = 1,
                    Address = new Address
                    {
                        Street = "Kulas Light",
                        Suite = "Apt. 556",
                        City = "Gwenborough",
                        ZipCode = "92998-3874",
                        Geo = new GeoLocation
                        {
                            Lat = -37.3159M,
                            Lng = 81.1496M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Ervin Howell",
                    UserName = "Antonette",
                    Email = "Shanna@melissa.tv",
                    PhoneNumber = "010-692-6593 x09125",
                    Website = "anastasia.net",
                    CompanyId = 2,
                    Address = new Address
                    {
                        Street = "Victor Plains",
                        Suite = "Suite 879",
                        City = "Wisokyburgh",
                        ZipCode = "90566-7771",
                        Geo = new GeoLocation
                        {
                            Lat = -43.9509M,
                            Lng = -34.4618M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Clementine Bauch",
                    UserName = "Samantha",
                    Email = "Nathan@yesenia.net",
                    PhoneNumber = "1-463-123-4447",
                    Website = "ramiro.info",
                    CompanyId = 3,
                    Address = new Address
                    {
                        Street = "Douglas Extension",
                        Suite = "Suite 847",
                        City = "McKenziehaven",
                        ZipCode = "59590-4157",
                        Geo = new GeoLocation
                        {
                            Lat = -68.6102M,
                            Lng = -47.0653M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Patricia Lebsack",
                    UserName = "Karianne",
                    Email = "Julianne.OConner@kory.org",
                    PhoneNumber = "493-170-9623 x156",
                    Website = "kale.biz",
                    CompanyId = 4,
                    Address = new Address
                    {
                        Street = "Hoeger Mall",
                        Suite = "Apt. 692",
                        City = "South Elvis",
                        ZipCode = "53919-4257",
                        Geo = new GeoLocation
                        {
                            Lat = 29.4572M,
                            Lng = -164.2990M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Chelsey Dietrich",
                    UserName = "Kamren",
                    Email = "Lucio_Hettinger@annie.ca",
                    PhoneNumber = "(254)954-1289",
                    Website = "demarco.info",
                    CompanyId = 5,
                    Address = new Address
                    {
                        Street = "Skiles Walks",
                        Suite = "Suite 351",
                        City = "Roscoeview",
                        ZipCode = "33263",
                        Geo = new GeoLocation
                        {
                            Lat = -31.8129M,
                            Lng = 62.5342M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Mrs. Dennis Schulist",
                    UserName = "Leopoldo_Corkery",
                    Email = "Karley_Dach@jasper.info",
                    PhoneNumber = "1-477-935-8478 x6430",
                    Website = "ola.org",
                    CompanyId = 6,
                    Address = new Address
                    {
                        Street = "Norberto Crossing",
                        Suite = "Apt. 950",
                        City = "South Christy",
                        ZipCode = "23505-1337",
                        Geo = new GeoLocation
                        {
                            Lat = -71.4197M,
                            Lng = 71.7478M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Kurtis Weissnat",
                    UserName = "Elwyn.Skiles",
                    Email = "Telly.Hoeger@billy.biz",
                    PhoneNumber = "210.067.6132",
                    Website = "elvis.io",
                    CompanyId = 7,
                    Address = new Address
                    {
                        Street = "Rex Trail",
                        Suite = "Suite 280",
                        City = "Howemouth",
                        ZipCode = "58804-1099",
                        Geo = new GeoLocation
                        {
                            Lat = 24.8918M,
                            Lng = 21.8984M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Nicholas Runolfsdottir V",
                    UserName = "Maxime_Nienow",
                    Email = "Sherwood@rosamond.me",
                    PhoneNumber = "586.493.6943 x140",
                    Website = "jacynthe.com",
                    CompanyId = 8,
                    Address = new Address
                    {
                        Street = "Ellsworth Summit",
                        Suite = "Suite 729",
                        City = "Aliyaview",
                        ZipCode = "45169",
                        Geo = new GeoLocation
                        {
                            Lat = -14.3990M,
                            Lng = -120.7677M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Glenna Reichert",
                    UserName = "Delphine",
                    Email = "Chaim_McDermott@dana.io",
                    PhoneNumber = "(775)976-6794 x41206",
                    Website = "conrad.com",
                    CompanyId = 9,
                    Address = new Address
                    {
                        Street = "Dayna Park",
                        Suite = "Suite 449",
                        City = "Bartholomebury",
                        ZipCode = "76495-3109",
                        Geo = new GeoLocation
                        {
                            Lat = 24.6463M,
                            Lng = -168.8889M
                        }
                    }
                },
                new ApplicationUser
                {
                    Name = "Clementina DuBuque",
                    UserName = "Moriah.Stanton",
                    Email = "Rey.Padberg@karina.biz",
                    PhoneNumber = "024-648-3804",
                    Website = "ambrose.net",
                    CompanyId = 10,
                    Address = new Address
                    {
                        Street = "Kattie Turnpike",
                        Suite = "Suite 198",
                        City = "Lebsackbury",
                        ZipCode = "31428-2261",
                        Geo = new GeoLocation
                        {
                            Lat = -38.2386M,
                            Lng = 57.2232M
                        }
                    }
                },
            };

            foreach (var userToAdd in usersToAdd)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userToAdd.UserName);
                if (user is null)
                {
                    user = userToAdd;
                    var result = await userManager.CreateAsync(user, "eren123");

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Kullanıcı eklenirken hata oluştu: {user.UserName}");
                    }

                    await userManager.AddToRolesAsync(user, rolesToAdd);
                }
            }
        }
    }
}
