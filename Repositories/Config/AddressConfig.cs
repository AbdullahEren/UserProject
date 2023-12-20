using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.AddressId);

            builder.Property(a => a.Street)
                .IsRequired();

            builder.Property(a => a.Suite)
                .IsRequired();

            builder.Property(a => a.City)
                .IsRequired();

            builder.HasOne(a => a.ApplicationUser)
                .WithOne(u => u.Address)
                .HasForeignKey<Address>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
