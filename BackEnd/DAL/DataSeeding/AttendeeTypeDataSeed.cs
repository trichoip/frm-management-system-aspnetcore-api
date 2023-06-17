using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class AttendeeTypeDataSeed
    {
        public static void SeedAttendeeType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendeeType>()
                .HasData(
                    new AttendeeType() { Id = 1, Name = "FRF" },
                    new AttendeeType() { Id = 2, Name = "FR" }, // Fresher
                    new AttendeeType() { Id = 3, Name = "CPL" }, 
                    new AttendeeType() { Id = 4, Name = "PFR" },
                    new AttendeeType() { Id = 5, Name = "CPLU" }
                );
        }
    }
}
