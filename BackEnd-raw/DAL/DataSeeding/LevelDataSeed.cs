using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class LevelDataSeed
    {
        public static void SeedLevel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>()
                .HasData(
                    new Level() { Id = 1, Name ="All level"},
                    new Level() { Id = 2, Name ="Fresher"},
                    new Level() { Id = 3, Name ="Intern"}
                );
        }
    }
}
