using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
  public static class ClassUpdateHistoryDataSeed
  {
    public static void SeedClassHistory(this ModelBuilder builder)
    {
      builder.Entity<ClassUpdateHistory>()
          .HasData(
              new ClassUpdateHistory
              {
                IdClass = 1,
                ModifyBy = 1,
                UpdateDate = new DateTime(2022, 11, 10)
              }
          );
    }
  }
}