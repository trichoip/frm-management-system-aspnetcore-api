using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class PermissionRightDataSeed
    {
        public static void SeedPermissionRight(this ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<PermissionRight>()
                .HasData(
                    new PermissionRight { IdRight = 1, IdRole = 1, IdPermission = 5 },
                    new PermissionRight { IdRight = 2, IdRole = 1, IdPermission = 5 },
                    new PermissionRight { IdRight = 3, IdRole = 1, IdPermission = 5 },
                    new PermissionRight { IdRight = 4, IdRole = 1, IdPermission = 5 },
                    new PermissionRight { IdRight = 5, IdRole = 1, IdPermission = 5 },
                    new PermissionRight { IdRight = 6, IdRole = 1, IdPermission = 5 }
                );
        }
    }
}
