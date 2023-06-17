using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class TrainingProgramDataSeed
    {
        public static void SeedTrainingProgram(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingProgram>()
                .HasData(
                    new TrainingProgram { Id = 1, Name = "C# Foundation", Status = 1 }
                );
        }
    }
}
