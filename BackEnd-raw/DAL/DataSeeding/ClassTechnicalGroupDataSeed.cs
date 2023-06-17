using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataSeeding
{
    public static class ClassTechnicalGroupDataSeed
    {
        public static void SeedClassTechnicalGroup(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassTechnicalGroup>()
                .HasData(
                    new ClassTechnicalGroup() { Id = 1, Name = "Java" },
                    new ClassTechnicalGroup() { Id = 2, Name = ".NET" },
                    new ClassTechnicalGroup() { Id = 3, Name = "FE" },
                    new ClassTechnicalGroup() { Id = 4, Name = "Android" },
                    new ClassTechnicalGroup() { Id = 5, Name = "CPP" },
                    new ClassTechnicalGroup() { Id = 6, Name = "Angular" },
                    new ClassTechnicalGroup() { Id = 7, Name = "React" },
                    new ClassTechnicalGroup() { Id = 8, Name = "Embedded" },
                    new ClassTechnicalGroup() { Id = 9, Name = "Out System" },
                    new ClassTechnicalGroup() { Id = 10, Name = "Sharepoint" },
                    new ClassTechnicalGroup() { Id = 11, Name = "iOS" },
                    new ClassTechnicalGroup() { Id = 12, Name = "Cobol" },
                    new ClassTechnicalGroup() { Id = 13, Name = "AUT" },
                    new ClassTechnicalGroup() { Id = 14, Name = "AI" },
                    new ClassTechnicalGroup() { Id = 15, Name = "Data" },
                    new ClassTechnicalGroup() { Id = 16, Name = "QA" },
                    new ClassTechnicalGroup() { Id = 17, Name = "Comtor" },
                    new ClassTechnicalGroup() { Id = 18, Name = "DevOps" },
                    new ClassTechnicalGroup() { Id = 19, Name = "SAP" },
                    new ClassTechnicalGroup() { Id = 20, Name = "ABAP" },
                    new ClassTechnicalGroup() { Id = 21, Name = "Go Lang" },
                    new ClassTechnicalGroup() { Id = 22, Name = "Flutter" },
                    new ClassTechnicalGroup() { Id = 23, Name = "ServiceNow" },
                    new ClassTechnicalGroup() { Id = 24, Name = "Front-End" },
                    new ClassTechnicalGroup() { Id = 25, Name = "Manual Test" },
                    new ClassTechnicalGroup() { Id = 26, Name = "Automation Test" },
                    new ClassTechnicalGroup() { Id = 27, Name = "C++" },
                    new ClassTechnicalGroup() { Id = 28, Name = "Python" },
                    new ClassTechnicalGroup() { Id = 29, Name = "IT" },
                    new ClassTechnicalGroup() { Id = 30, Name = "OCA8" },
                    new ClassTechnicalGroup() { Id = 31, Name = "BA" },
                    new ClassTechnicalGroup() { Id = 32, Name = "APM" },
                    new ClassTechnicalGroup() { Id = 33, Name = "DSA" },
                    new ClassTechnicalGroup() { Id = 34, Name = "FIF" },
                    new ClassTechnicalGroup() { Id = 35, Name = "STE" },
                    new ClassTechnicalGroup() { Id = 36, Name = "Flexcube" },
                    new ClassTechnicalGroup() { Id = 37, Name = "CLOUD" },
                    new ClassTechnicalGroup() { Id = 38, Name = "PHP" },
                    new ClassTechnicalGroup() { Id = 39, Name = "NodeJS" },
                    new ClassTechnicalGroup() { Id = 40, Name = "Security Engineer" },
                    new ClassTechnicalGroup() { Id = 41, Name = "Microsoft Power Platform" },
                    new ClassTechnicalGroup() { Id = 42, Name = "Data Engineer" },
                    new ClassTechnicalGroup() { Id = 43, Name = "Sitecore" },
                    new ClassTechnicalGroup() { Id = 44, Name = "Agile" },
                    new ClassTechnicalGroup() { Id = 45, Name = "React Native" },
                    new ClassTechnicalGroup() { Id = 46, Name = "Certificate" },
                    new ClassTechnicalGroup() { Id = 47, Name = "SAP,ABAP" },
                    new ClassTechnicalGroup() { Id = 48, Name = "Mobile" },
                    new ClassTechnicalGroup() { Id = 49, Name = "WinApp" },
                    new ClassTechnicalGroup() { Id = 50, Name = "PHP" },
                    new ClassTechnicalGroup() { Id = 51, Name = "RPA" },
                    new ClassTechnicalGroup() { Id = 52, Name = "Erlang" },
                    new ClassTechnicalGroup() { Id = 53, Name = "Fullstack Java" },
                    new ClassTechnicalGroup() { Id = 54, Name = "Fullstack .NET" },
                    new ClassTechnicalGroup() { Id = 55, Name = "Java Standard" },
                    new ClassTechnicalGroup() { Id = 56, Name = ".NET standard" },
                    new ClassTechnicalGroup() { Id = 57, Name = "Golang" },
                    new ClassTechnicalGroup() { Id = 58, Name = "C++/Linux" },
                    new ClassTechnicalGroup() { Id = 59, Name = "AEM" },
                    new ClassTechnicalGroup() { Id = 60, Name = "JP" }
                );
        }
    }
}
