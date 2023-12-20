using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    

    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.CatchPhrase)
                .IsRequired();

            builder.Property(c => c.Bs)
                .IsRequired();


            builder.HasData(
                    new Company
                    {
                        CompanyId = 1,
                        Name = "Romaguera-Crona",
                        CatchPhrase = "Multi-layered client-server neural-net",
                        Bs = "harness real-time e-markets"
                    },
                    new Company
                    {
                        CompanyId = 2,
                        Name = "Deckow-Crist",
                        CatchPhrase = "Proactive didactic contingency",
                        Bs = "synergize scalable supply-chains"
                    },
                    new Company
                    {
                        CompanyId = 3,
                        Name = "Romaguera-Jacobson",
                        CatchPhrase = "Face to face bifurcated interface",
                        Bs = "e-enable strategic applications"
                    },
                    new Company
                    {
                        CompanyId = 4,
                        Name = "Robel-Corkery",
                        CatchPhrase = "Multi-tiered zero tolerance productivity",
                        Bs = "transition cutting-edge web services"
                    },
                    new Company
                    {
                        CompanyId = 5,
                        Name = "Keebler LLC",
                        CatchPhrase = "User-centric fault-tolerant solution",
                        Bs = "revolutionize end-to-end systems"
                    },
                    new Company
                    {
                        CompanyId = 6,
                        Name = "Considine-Lockman",
                        CatchPhrase = "Synchronised bottom-line interface",
                        Bs = "e-enable innovative applications"
                    },
                    new Company
                    {
                        CompanyId = 7,
                        Name = "Johns Group",
                        CatchPhrase = "Configurable multimedia task-force",
                        Bs = "generate enterprise e-tailers"
                    },
                    new Company
                    {
                        CompanyId = 8,
                        Name = "Abernathy Group",
                        CatchPhrase = "Implemented secondary concept",
                        Bs = "e-enable extensible e-tailers"
                    },
                    new Company
                    {
                        CompanyId = 9,
                        Name = "Yost and Sons",
                        CatchPhrase = "Switchable contextually-based project",
                        Bs = "aggregate real-time technologies"
                    },
                    new Company
                    {
                        CompanyId = 10,
                        Name = "Hoeger LLC",
                        CatchPhrase = "Centralized empowering task-force",
                        Bs = "target end-to-end models"
                    });
        }
    }

}
