using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class RightDataSeed
    {
        public static void SeedRight(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Right>()
                .HasData(
                    new Right { Id = 1, Name = "Syllabus" },
                    new Right { Id = 2, Name = "Training program" },
                    new Right { Id = 3, Name = "Class" },
                    new Right { Id = 4, Name = "Learning material" },
                    new Right { Id = 5, Name = "User" },
                    new Right { Id = 6, Name = "Training calendar" }
                );
        }
    }
}
