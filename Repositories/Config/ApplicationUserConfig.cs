using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json.Linq;

namespace Repositories.Config
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserConfig(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();

            builder.HasOne(u => u.Company)
                   .WithMany()
                   .HasForeignKey(u => u.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.CompanyId).IsRequired(false);
        }

    }
}
