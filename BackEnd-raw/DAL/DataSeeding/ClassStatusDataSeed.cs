using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class ClassStatusDataSeed
    {
        public static void SeedClassStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassStatus>()
                .HasData(
                    new ClassStatus() { Id = 1, Name = "Draft" },
                    new ClassStatus() { Id = 2, Name = "Reviewing" },
                    new ClassStatus() { Id = 3, Name = "Approving" },
                    new ClassStatus() { Id = 4, Name = "Start" },
                    new ClassStatus() { Id = 5, Name = "Done" },
                    new ClassStatus() { Id = 6, Name = "Delayed" },
                    new ClassStatus() { Id = 7, Name = "Opened" },
                    new ClassStatus() { Id = 8, Name = "Active" },
                    new ClassStatus() { Id = 9, Name = "Inactive" }
                );
        }
    }
}
