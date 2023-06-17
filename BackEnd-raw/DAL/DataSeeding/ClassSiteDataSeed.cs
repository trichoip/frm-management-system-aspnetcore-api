using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class ClassSiteDataSeed
    {
        public static void SeedClassSite(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSite>()
                .HasData(
                    new ClassSite() { Id = 1, Site = "HN" }, // Hà Nội
                    new ClassSite() { Id = 2, Site = "HCM" }, // Hồ Chí Minh
                    new ClassSite() { Id = 3, Site = "DN" }, // Đà Nẵng
                    new ClassSite() { Id = 4, Site = "CT" }, // Cần Thơ
                    new ClassSite() { Id = 5, Site = "QN" } // Quy Nhơn
                );
        }
    }
}
