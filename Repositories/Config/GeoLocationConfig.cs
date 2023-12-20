
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class GeoLocationConfig : IEntityTypeConfiguration<GeoLocation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GeoLocation> builder)
        {
            builder.HasKey(g => g.GeoLocationId);

            builder.Property(g => g.Lat)
                .IsRequired();

            builder.Property(g => g.Lng)
                .IsRequired();

            builder.HasOne(g => g.Address)
                .WithOne(a => a.Geo)
                .HasForeignKey<GeoLocation>(g => g.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
