using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class PortsConfiguration : IEntityTypeConfiguration<Ports>
    {
        public void Configure(EntityTypeBuilder<Ports> builder)
        {
            builder.HasData
            (
            new Ports
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991053"),
                Title = "Port of Baltimore",
                Country = "USA",
                Capacity = 130,
                Type = "Maritime"
            },
            new Ports
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2a70"),
                Title = "Northern river port",
                Country = "Russia",
                Capacity = 75,
                Type = "River"
            },
            new Ports
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c294c"),
                Title = "Novorossiysk seaport",
                Country = "Russia",
                Capacity = 100,
                Type = "Maritime"

            }
            );
            
        }
    }
}
