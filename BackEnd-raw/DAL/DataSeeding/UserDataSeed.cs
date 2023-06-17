using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataSeeding
{
    public static class UserDataSeed
    {
        public static void SeedUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        ID = 1,
                        UserName = "superadmin@fsoft.com",
                        Password = "$2a$11$k7zKp9cQOIE3/c22YdD29O52l8x.9bbji4kJOPJ3Jy.f4kIUYIQ0G",
                        FullName = "Super Admin",
                        Image = null,
                        DateOfBirth = new DateTime(2000, 8, 2),
                        Gender = 'M',
                        Phone = "0123456789",
                        Email = "superadmin@fsoft.com",
                        Address = "123 đường 456",
                        ResetPasswordOtp = null,
                        Status = 1,
                        IdRole = 1,
                    },
                    new User()
                    {
                        ID = 2,
                        UserName = "classadmin@fsoft.com",
                        Password = "$2a$11$IWL97xH2L60fhHMoo38msOYC7ZsP6GsrpnO.CLS04IRNBkqs8TdWS",
                        FullName = "Class Admin",
                        Image = null,
                        DateOfBirth = new DateTime(2000, 8, 2),
                        Gender = 'F',
                        Phone = "0123456789",
                        Email = "classadmin@fsoft.com",
                        Address = "123 đường 456",
                        ResetPasswordOtp = null,
                        Status = 1,
                        IdRole = 2,
                    },
                    new User()
                    {
                        ID = 3,
                        UserName = "trainer@fsoft.com",
                        Password = "$2a$11$4/mkPNwz0l/.e7zXfRT69eKsP327tqz10Ldf5s0iWAZLNCWRRRrxK",
                        FullName = "Trainer",
                        Image = null,
                        DateOfBirth = new DateTime(2000, 8, 2),
                        Gender = 'M',
                        Phone = "0123456789",
                        Email = "trainer@fsoft.com",
                        Address = "123 đường 456",
                        ResetPasswordOtp = null,
                        Status = 1,
                        IdRole = 3,
                    },
                    new User()
                    {
                        ID = 4,
                        UserName = "student@fsoft.com",
                        Password = "$2a$11$WTeAE4MdAkR4ZtVooCdZkuuzam5sdxDTpm1VJqL/RFIEJNLJk.PX2",
                        FullName = "Student",
                        Image = null,
                        DateOfBirth = new DateTime(2000, 8, 2),
                        Gender = 'M',
                        Phone = "0123456789",
                        Email = "student@fsoft.com",
                        Address = "123 đường 456",
                        ResetPasswordOtp = null,
                        Status = 1,
                        IdRole = 4,
                    }
                );
        }
    }
}
