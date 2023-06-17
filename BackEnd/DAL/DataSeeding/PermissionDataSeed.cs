using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class PermissionDataSeed
    {
        public static void SeedPermission(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasData(
                    new Permission() { Id = 1, Name = "Access denied" },
                    new Permission() { Id = 2, Name = "View" },
                    new Permission() { Id = 3, Name = "Modify" },
                    new Permission() { Id = 4, Name = "Create" },
                    new Permission() { Id = 5, Name = "Full access" },
                    new Permission() { Id = 6, Name = "Delete while viewing" }
                );
        }
    }
}
