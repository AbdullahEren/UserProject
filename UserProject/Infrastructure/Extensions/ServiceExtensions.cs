using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using UserProject.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Services.Hubs;
using MassTransit;
using Services.Events.EventConsumer;
using Services.Events.EventBus;


namespace UserProject.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbHost = configuration["DB_HOST"];
            var dbPort = configuration["DB_PORT"];
            var dbName = configuration["DB_NAME"];
            var dbUser = configuration["DB_USER"];
            var dbPassword = configuration["DB_PASSWORD"];

            var mssqlconnection = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True";

            services.AddDbContext<RepositoryContext>(options =>
                           options.UseSqlServer(mssqlconnection, b => b.MigrationsAssembly("UserProject")));

        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddSingleton<ICacheService, RedisManager>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddTransient<NotificationHub>();
            services.AddTransient<IEventBus, EventBus>();

        }

        public static void CorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(policy =>
                            policy.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                }
            );
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void RedisConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                var host = configuration["REDIS_HOST"];
                var port = configuration["REDIS_PORT"];
                var connection = $"{host}:{port}";
                options.Configuration = connection;
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.AddConsumer<UserCreatedEventConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {

                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(configuration["MessageBroker:Username"]);
                        h.Password(configuration["MessageBroker:Password"]);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
        }

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<ApplicationRole> roleManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();

            var rolesToAdd = new string[] { "Admin", "Admin", "User", "User", "User", "User", "User", "User", "User", "User" };

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
                var userIndex = Array.IndexOf(usersToAdd, userToAdd);
                var userCount = await userManager.Users.CountAsync();
                if (user is null && userCount < 10)
                {
                    user = userToAdd;
                    var result = await userManager.CreateAsync(user, "eren123");

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Kullanıcı eklenirken hata oluştu: {user.UserName}");
                    }

                    var userRole = rolesToAdd[userIndex];
                    await userManager.AddToRoleAsync(user, userRole);
                }
            }
        }
    }
}
