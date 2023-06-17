using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class ClassUniversityCodeDataSeed
    {
        public static void SeedClassUniversityCode(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassUniversityCode>()
                .HasData(
                    new ClassUniversityCode() { Id = 1, UniversityCode = "ALL" }
                );
        }
    }
}
