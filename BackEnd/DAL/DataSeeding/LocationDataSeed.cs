using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class LocationDataSeed
    {
        public static void SeedLocation(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasData(
                    new Location() { Id = 1, Name = "FTown 1" },
                    new Location() { Id = 2, Name = "FTown 2" },
                    new Location() { Id = 3, Name = "FTown 3" }
                );
        }
    }
}
