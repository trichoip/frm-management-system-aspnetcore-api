using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class DeliveryTypeDataSeed
    {
        public static void SeedDeliveryType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryType>()
                .HasData
                (
                new DeliveryType()
                {
                    Id = 1,
                    Name = "Assignment/Lab"
                },
                new DeliveryType()
                {
                    Id = 2,
                    Name = "Concept/Lecture"
                }, new DeliveryType()
                {
                    Id = 3,
                    Name = "Guide/Review"
                }, new DeliveryType()
                {
                    Id = 4,
                    Name = "Test/Quiz"
                }, new DeliveryType()
                {
                    Id = 5,
                    Name = "Exam"
                }, new DeliveryType()
                {
                    Id = 6,
                    Name = "Seminar/Workshop"
                }
                );
        }
    }
}
