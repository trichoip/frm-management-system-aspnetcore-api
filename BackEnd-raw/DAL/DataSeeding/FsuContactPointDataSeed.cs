using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class FsuContactPointDataSeed
    {
        public static void SeedFsuContactPoint(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FSUContactPoint>()
                .HasData(
                    new FSUContactPoint() { Id = 1, IdFSU = 1, Contact = "BaCH@fsoft.com.vn", Status = 1 }, // FK: FHM
                    new FSUContactPoint() { Id = 2, IdFSU = 2, Contact = "0912345678", Status = 1 } // FK: FU
                );
            // status: 1 = active
            // status: 2 = inactive
        }
    }
}
