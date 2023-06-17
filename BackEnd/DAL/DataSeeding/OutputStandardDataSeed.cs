using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class OutputStandardDataSeed
    {
        public static void SeedOutputStandard(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutputStandard>()
                .HasData(
                    new OutputStandard() { Id = 1,Name = "H4SD"},
                    new OutputStandard() { Id = 2, Name = "K6SD" },
                    new OutputStandard() { Id = 3, Name = "H6SD" },
                    new OutputStandard() { Id = 4, Name = "H1ST" },
                    new OutputStandard() { Id = 5, Name = "H2SD" },
                    new OutputStandard() { Id = 6, Name = "K4SD" },
                    new OutputStandard() { Id = 7, Name = "K3SD " }
                );
        }
    }
}
