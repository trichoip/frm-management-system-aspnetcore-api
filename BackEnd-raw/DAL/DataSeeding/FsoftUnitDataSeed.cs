using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class FsoftUnitDataSeed
    {
        public static void SeedFsoftunit(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FsoftUnit>()
                .HasData(
                    new FsoftUnit() { Id = 1, Name = "FHM", Status = 1},
                    new FsoftUnit() { Id = 2, Name = "FU", Status = 1},
                    new FsoftUnit() { Id = 3, Name = "FPTN", Status = 1 }
                );
            // status: 1 = active
            // status: 2 = inactive
        }
    }
}
