using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
  public static class RoleDataSeed
  {
    public static void SeedRole(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Role>()
          .HasData(
              new Role() { Id = 1, Name = "Super Admin" },
              new Role() { Id = 2, Name = "Class Admin" },
              new Role() { Id = 3, Name = "Trainer" },
              new Role() { Id = 4, Name = "Student" }
          );
    }
  }
}
