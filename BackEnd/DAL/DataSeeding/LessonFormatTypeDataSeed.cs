using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataSeeding
{
    public static class LessonFormatTypeDataSeed
    {
        public static void SeedLessonFormatType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormatType>()
                .HasData
                (
                    new FormatType() { Id = 1, Name = "Offline" },
                    new FormatType() { Id = 2, Name = "Online" }
                );
        }
    }
}
