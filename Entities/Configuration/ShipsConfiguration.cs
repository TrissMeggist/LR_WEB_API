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
    public class ShipsConfiguration : IEntityTypeConfiguration<Ships>
    {
        public void Configure(EntityTypeBuilder<Ships> builder)
        {
            builder.HasData
            (
            new Ships
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497ca8"),
                Title = "Baikal",
                Class = "screw ships class M",
                PortsId = new Guid("3d490a70-94ce-4d15-9494-5248280c2a70")

            },
            new Ships
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb8c0"),
                Title = "Maxim Gorkiy",
                Class = "cruise ship class + 1A1",
                PortsId = new Guid("3d490a70-94ce-4d15-9494-5248280c294c")

            },
            new Ships
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a84793c1"),
                Title = "USS Nimitz (CVN-68)",
                Class = "Nimitz-class aircraft carrier",
                PortsId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991053")

            });
        }
    }
}
