using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
  public static class TrainingProgramHistoryDataSeed
  {
    public static void SeedTrainingProgramHistory(this ModelBuilder buider)
    {
      buider.Entity<HistoryTrainingProgram>()
              .HasData
              (
                  new HistoryTrainingProgram
                  {
                    IdProgram = 1,
                    IdUser = 1,
                    ModifiedOn = new DateTime(2022, 11, 10)
                  }
              );
    }
  }
}