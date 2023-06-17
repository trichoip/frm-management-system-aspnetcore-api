using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class ClassDataSeed
    {
        public static void SeedClass(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasData(
                    new Class
                    {
                        Id = 1,
                        Name = "Class name",
                        ClassCode = "Class code",
                        Status = 1,
                        StartTimeLearning = new TimeSpan(8, 0, 0),
                        EndTimeLearing = new TimeSpan(12, 0, 0),
                        ReviewedBy = 1,
                        ReviewedOn = new DateTime(2022, 11, 5),
                        CreatedBy = 1,
                        CreatedOn = new DateTime(2022, 11, 4),
                        ApprovedBy = 1,
                        ApprovedOn = new DateTime(2022, 11, 6),
                        PlannedAtendee = 20,
                        ActualAttendee = 18,
                        AcceptedAttendee = 18,
                        CurrentSession = 1,
                        CurrentUnit = 1,
                        StartYear = 2022,
                        StartDate = new DateTime(2022, 11, 7),
                        EndDate = new DateTime(2022, 12, 1),
                        ClassNumber = 1,
                        IdProgram = 1,
                        IdTechnicalGroup = 1,
                        IdFSU = 1,
                        IdFSUContact = 1,
                        IdStatus = 1,
                        IdSite = 1,
                        IdUniversity = 1,
                        IdFormatType = 1,
                        IdProgramContent = 1,
                        IdAttendeeType = 1
                    }
                );
        }
    }
}
