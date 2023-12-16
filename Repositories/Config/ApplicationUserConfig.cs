using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Phone).IsRequired();

            builder.HasOne(u => u.Company)
                   .WithMany()
                   .HasForeignKey(u => u.CompanyId)
                   .IsRequired();

            builder.HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Name = "Leanne Graham",
                    Username = "Bret",
                    Email = "Sincere@april.biz",
                    Phone = "1-770-736-8031 x56442",
                    Website = "hildegard.org",
                    Address = new Address
                    {
                        Street = "Kulas Light",
                        Suite = "Apt. 556",
                        City = "Gwenborough",
                        ZipCode = "92998-3874",
                        Geo = new GeoLocation { Lat = -37.3159m, Lng = 81.1496m }
                    },
                    CompanyId = 1
                },
                new ApplicationUser
                {
                    Id = 2,
                    Name = "Ervin Howell",
                    Username = "Antonette",
                    Email = "Shanna@melissa.tv",
                    Phone = "010-692-6593 x09125",
                    Website = "anastasia.net",
                    Address = new Address
                    {
                        Street = "Victor Plains",
                        Suite = "Suite 879",
                        City = "Wisokyburgh",
                        ZipCode = "90566-7771",
                        Geo = new GeoLocation { Lat = -43.9509m, Lng = -34.4618m }
                    },
                    CompanyId = 2
                },
                new ApplicationUser
                {
                    Id = 3,
                    Name = "Clementine Bauch",
                    Username = "Samantha",
                    Email = "Nathan@yesenia.net",
                    Phone = "1-463-123-4447",
                    Website = "ramiro.info",
                    Address = new Address
                    {
                        Street = "Douglas Extension",
                        Suite = "Suite 847",
                        City = "McKenziehaven",
                        ZipCode = "59590-4157",
                        Geo = new GeoLocation { Lat = -68.6102m, Lng = -47.0653m }
                    },
                    CompanyId = 3
                },
                new ApplicationUser
                {
                    Id = 4,
                    Name = "Patricia Lebsack",
                    Username = "Karianne",
                    Email = "Julianne.OConner@kory.org",
                    Phone = "493-170-9623 x156",
                    Website = "kale.biz",
                    Address = new Address
                    {
                        Street = "Hoeger Mall",
                        Suite = "Apt. 692",
                        City = "South Elvis",
                        ZipCode = "53919-4257",
                        Geo = new GeoLocation { Lat = 29.4572m, Lng = -164.2990m }
                    },
                    CompanyId = 4
                },
                new ApplicationUser
                {
                    Id = 5,
                    Name = "Chelsey Dietrich",
                    Username = "Kamren",
                    Email = "Lucio_Hettinger@annie.ca",
                    Phone = "(254)954-1289",
                    Website = "demarco.info",
                    Address = new Address
                    {
                        Street = "Skiles Walks",
                        Suite = "Suite 351",
                        City = "Roscoeview",
                        ZipCode = "33263",
                        Geo = new GeoLocation { Lat = -31.8129m, Lng = 62.5342m }
                    },
                    CompanyId = 5
                },
                new ApplicationUser
                {
                    Id = 6,
                    Name = "Mrs. Dennis Schulist",
                    Username = "Leopoldo_Corkery",
                    Email = "Karley_Dach@jasper.info",
                    Phone = "1-477-935-8478 x6430",
                    Website = "ola.org",
                    Address = new Address
                    {
                        Street = "Norberto Crossing",
                        Suite = "Apt. 950",
                        City = "South Christy",
                        ZipCode = "23505-1337",
                        Geo = new GeoLocation { Lat = -71.4197m, Lng = 71.7478m }
                    },
                    CompanyId = 6
                },
                new ApplicationUser
                {
                    Id = 7,
                    Name = "Kurtis Weissnat",
                    Username = "Elwyn.Skiles",
                    Email = "Telly.Hoeger@billy.biz",
                    Phone = "210.067.6132",
                    Website = "elvis.io",
                    Address = new Address
                    {
                        Street = "Rex Trail",
                        Suite = "Suite 280",
                        City = "Howemouth",
                        ZipCode = "58804-1099",
                        Geo = new GeoLocation { Lat = 24.8918m, Lng = 21.8984m }
                    },
                    CompanyId = 7
                },
                new ApplicationUser
                {
                    Id = 8,
                    Name = "Nicholas Runolfsdottir V",
                    Username = "Maxime_Nienow",
                    Email = "Sherwood@rosamond.me",
                    Phone = "586.493.6943 x140",
                    Website = "jacynthe.com",
                    Address = new Address
                    {
                        Street = "Ellsworth Summit",
                        Suite = "Suite 729",
                        City = "Aliyaview",
                        ZipCode = "45169",
                        Geo = new GeoLocation { Lat = -14.3990m, Lng = -120.7677m }
                    },
                    CompanyId = 8
                },
                new ApplicationUser
                {
                    Id = 9,
                    Name = "Glenna Reichert",
                    Username = "Delphine",
                    Email = "Chaim_McDermott@dana.io",
                    Phone = "(775)976-6794 x41206",
                    Website = "conrad.com",
                    Address = new Address
                    {
                        Street = "Dayna Park",
                        Suite = "Suite 449",
                        City = "Bartholomebury",
                        ZipCode = "76495-3109",
                        Geo = new GeoLocation { Lat = 24.6463m, Lng = -168.8889m }
                    },
                    CompanyId = 9
                },
                new ApplicationUser
                {
                    Id = 10,
                    Name = "Clementina DuBuque",
                    Username = "Moriah.Stanton",
                    Email = "Rey.Padberg@karina.biz",
                    Phone = "024-648-3804",
                    Website = "ambrose.net",
                    Address = new Address
                    {
                        Street = "Kattie Turnpike",
                        Suite = "Suite 198",
                        City = "Lebsackbury",
                        ZipCode = "31428-2261",
                        Geo = new GeoLocation { Lat = -38.2386m, Lng = 57.2232m }
                    },
                    CompanyId = 10
                }
            );


        }
    }
}
