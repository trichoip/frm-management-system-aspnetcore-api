using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class ClassFormatTypeDataSeed
    {
        public static void SeedClassFormatType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassFormatType>()
                .HasData(
                    new ClassFormatType() { Id = 1, Name = "Offline" },
                    new ClassFormatType() { Id = 2, Name = "Online" },
                    new ClassFormatType() { Id = 3, Name = "OJT" },
                    new ClassFormatType() { Id = 4, Name = "Virtual Training" },
                    new ClassFormatType() { Id = 5, Name = "Blended" }
                );
        }
    }
}
