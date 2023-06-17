using DAL.DataSeeding;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
  public class FRMDbContext : DbContext
  {

    public FRMDbContext()
    {

    }

    public FRMDbContext(DbContextOptions<FRMDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleRight> RoleRights { get; set; }
    public DbSet<Right> Rights { get; set; }
    public DbSet<PermissionRight> PermissionRights { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<HistoryMaterial> HistoryMaterials { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<DeliveryType> DeliveryTypes { get; set; }
    public DbSet<FormatType> FormatTypes { get; set; }
    public DbSet<OutputStandard> OutputStandards { get; set; }
    public DbSet<Syllabus> Syllabi { get; set; }
    public DbSet<SyllabusTrainer> SyllabusTrainers { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<HistorySyllabus> HistorySyllabi { get; set; }
    public DbSet<AssignmentSchema> assignmentSchemas { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Curriculum> Curricula { get; set; }
    public DbSet<HistoryTrainingProgram> HistoryTrainingPrograms { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<AttendeeType> AttendeeTypes { get; set; }
    public DbSet<ClassProgramCode> ClassProgramCodes { get; set; }
    public DbSet<ClassSite> ClassSites { get; set; }
    public DbSet<ClassStatus> ClassStatuses { get; set; }
    public DbSet<ClassTechnicalGroup> ClassTechnicalGroups { get; set; }
    public DbSet<ClassUniversityCode> ClassUniversityCodes { get; set; }
    public DbSet<FsoftUnit> FsoftUnits { get; set; }
    public DbSet<FSUContactPoint> FSUContactPoints { get; set; }
    public DbSet<ClassFormatType> ClassFormatTypes { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ClassLocation> ClassLocations { get; set; }
    public DbSet<ClassSelectedDate> ClassSelectedDates { get; set; }
    public DbSet<ClassUpdateHistory> ClassUpdateHistories { get; set; }
    public DbSet<ClassTrainee> ClassTrainees { get; set; }
    public DbSet<ClassAdmin> ClassAdmins { get; set; }
    public DbSet<ClassMentor> classMentors { get; set; }
    public DbSet<TrainingProgram> TrainingPrograms { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();

        string connectionString = config["ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseSqlServer(connectionString);
      }
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      #region User
      modelBuilder.Entity<User>()
          .HasKey("ID");

      modelBuilder.Entity<User>()
          .Property("UserName")
          .HasColumnType("varchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Password")
          .HasColumnType("nvarchar")
          .HasMaxLength(500)
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("FullName")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Image")
          .IsRequired(false);

      modelBuilder.Entity<User>()
          .Property("DateOfBirth")
          .HasColumnType("date")
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Gender")
          .HasColumnType("char")
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Status")
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Phone")
          .HasColumnType("char")
          .HasMaxLength(10);

      modelBuilder.Entity<User>()
          .Property("Email")
          .HasColumnType("varchar")
          .HasMaxLength(100)
          .IsRequired();

      modelBuilder.Entity<User>()
          .Property("Address")
          .HasColumnType("nvarchar")
          .HasMaxLength(100);

      modelBuilder.Entity<User>()
          .Property("ResetPasswordOtp")
          .HasColumnType("nvarchar")
          .HasMaxLength(6)
          .IsRequired(false);

      modelBuilder.Entity<User>()
    .HasOne<Role>(u => u.Role)
    .WithMany(r => r.Users)
    .HasForeignKey(u => u.IdRole);
      #endregion

      #region Role
      modelBuilder.Entity<Role>()
          .HasKey("Id");
      modelBuilder.Entity<Role>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();
      #endregion

      #region RoleRight
      modelBuilder.Entity<RoleRight>()
          .HasKey("IDRight", "IDRole");

      modelBuilder.Entity<RoleRight>()
          .HasOne<Role>(rr => rr.Role)
          .WithMany(r => r.RoleRights)
          .HasForeignKey(rr => rr.IDRole);

      modelBuilder.Entity<RoleRight>()
          .HasOne<Right>(rr => rr.Right)
          .WithMany(r => r.RoleRights)
          .HasForeignKey(rr => rr.IDRight);
      #endregion

      #region Right
      modelBuilder.Entity<Right>()
          .HasKey("Id");

      modelBuilder.Entity<Right>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();
      #endregion

      #region PermissionRight
      modelBuilder.Entity<PermissionRight>()
          .HasKey("IdRight", "IdRole");

      modelBuilder.Entity<PermissionRight>()
          .HasOne<Right>(pr => pr.Right)
          .WithMany(r => r.PermissionRights)
          .HasForeignKey(pr => pr.IdRight);

      modelBuilder.Entity<PermissionRight>()
          .HasOne<Role>(pr => pr.Role)
          .WithMany(r => r.PermissionRights)
          .HasForeignKey(pr => pr.IdRole);

      modelBuilder.Entity<PermissionRight>()
          .HasOne<Permission>(pr => pr.Permission)
          .WithMany(p => p.PermissionRights)
          .HasForeignKey(pr => pr.IdPermission);
      #endregion

      #region Permission
      modelBuilder.Entity<Permission>()
          .HasKey("Id");

      modelBuilder.Entity<Permission>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();
      #endregion

      #region Material
      modelBuilder.Entity<Material>()
          .HasKey("Id");

      modelBuilder.Entity<Material>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Material>()
          .Property("HyperLink")
          .HasColumnType("nvarchar(MAX)")
          .IsRequired();
      modelBuilder.Entity<Material>()
          .Property("Status")
          .HasColumnType("int")
          .IsRequired(false);

      modelBuilder.Entity<Material>()
          .HasOne<Lesson>(m => m.Lesson)
          .WithMany(l => l.Materials)
          .HasForeignKey(m => m.IdLesson);
      #endregion

      #region HistoryMaterial
      modelBuilder.Entity<HistoryMaterial>()
          .HasKey("IdUser", "IdMaterial", "ModifiedOn");

      modelBuilder.Entity<HistoryMaterial>()
          .Property("ModifiedOn")
          .HasColumnType("datetime")
          .IsRequired();

      modelBuilder.Entity<HistoryMaterial>()
          .HasOne<Material>(hm => hm.Material)
          .WithMany(m => m.HistoryMaterials)
          .HasForeignKey(hm => hm.IdMaterial);

      modelBuilder.Entity<HistoryMaterial>()
          .HasOne<User>(hm => hm.User)
          .WithMany(u => u.HistoryMaterials)
          .HasForeignKey(hm => hm.IdUser);
      modelBuilder.Entity<HistoryMaterial>()
          .Property("Action")
          .HasColumnType("varchar(50)")
          .IsRequired();
      #endregion

      #region Lesson
      modelBuilder.Entity<Lesson>()
          .HasKey("Id");

      modelBuilder.Entity<Lesson>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50).IsRequired();

      modelBuilder.Entity<Lesson>()
          .Property("Duration")
          .IsRequired();

      modelBuilder.Entity<Lesson>()
          .HasOne<DeliveryType>(l => l.DeliveryType)
          .WithMany(dt => dt.Lessons)
          .HasForeignKey(l => l.IdDeliveryType)
          .IsRequired();

      modelBuilder.Entity<Lesson>()
          .HasOne<FormatType>(l => l.FormatType)
          .WithMany(ft => ft.Lessons)
          .HasForeignKey(l => l.IdFormatType)
          .IsRequired();

      modelBuilder.Entity<Lesson>()
          .HasOne<OutputStandard>(l => l.OutputStandard)
          .WithMany(os => os.Lessons)
          .HasForeignKey(l => l.IdOutputStandard)
          .IsRequired();

      modelBuilder.Entity<Lesson>()
          .HasOne<Unit>(l => l.Unit)
          .WithMany(u => u.Lessons)
          .HasForeignKey(l => l.IdUnit)
          .IsRequired();
      modelBuilder.Entity<Lesson>()
          .Property("Status")
          .HasColumnType("int")
          .IsRequired(false);

      #endregion

      #region DeliveryType
      modelBuilder.Entity<DeliveryType>()
          .HasKey("Id");

      modelBuilder.Entity<DeliveryType>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region FormatType
      modelBuilder.Entity<FormatType>()
          .HasKey("Id");

      modelBuilder.Entity<FormatType>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region OutputStandard
      modelBuilder.Entity<OutputStandard>()
          .HasKey("Id");

      modelBuilder.Entity<OutputStandard>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Syllabus
      modelBuilder.Entity<Syllabus>()
          .HasKey("Id");

      modelBuilder.Entity<Syllabus>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Syllabus>()
          .Property("Code")
          .HasColumnType("varchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Syllabus>()
          .Property("AttendeeNumber")
          .HasColumnType("int")
          .IsRequired();

      modelBuilder.Entity<Syllabus>()
    .Property("Version")
    .HasColumnType("float")
    .IsRequired();

      modelBuilder.Entity<Syllabus>()
          .Property("Technicalrequirement")
          .HasColumnType("NText")
          .IsRequired(false);

      modelBuilder.Entity<Syllabus>()
          .Property("CourseObjectives")
          .HasColumnType("nvarchar(MAX)")
          .IsRequired(false);

      modelBuilder.Entity<Syllabus>()
          .Property("TrainingPrinciple")
          .HasColumnType("NText")
          .IsRequired(false);

      modelBuilder.Entity<Syllabus>()
          .Property("Description")
          .HasColumnType("NText")
          .IsRequired(false);

      modelBuilder.Entity<Syllabus>()
          .Property("HyperLink")
          .HasColumnType("nvarchar(MAX)")
          .IsRequired(false);

      modelBuilder.Entity<Syllabus>()
          .Property("Status")
          .IsRequired();

      modelBuilder.Entity<Syllabus>()
          .Property("Version")
          .IsRequired();

      modelBuilder.Entity<Syllabus>()
          .HasOne<Level>(s => s.Level)
          .WithMany(l => l.Syllabi)
          .HasForeignKey(s => s.IdLevel);

      modelBuilder.Entity<Syllabus>()
          .HasMany(s => s.Sessions)
          .WithOne(x => x.Syllabus);


      #endregion

      #region Level
      modelBuilder.Entity<Level>()
          .HasKey("Id");

      modelBuilder.Entity<Level>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();
      #endregion

      #region Syllabus Trainer
      modelBuilder.Entity<SyllabusTrainer>()
          .HasKey("IdUser", "IdSyllabus");

      modelBuilder.Entity<SyllabusTrainer>()
          .HasOne<User>(st => st.User)
          .WithMany(u => u.SyllabusTrainers)
          .HasForeignKey(st => st.IdUser);

      modelBuilder.Entity<SyllabusTrainer>()
          .HasOne<Syllabus>(st => st.Syllabus)
          .WithMany(s => s.SyllabusTrainers)
          .HasForeignKey(st => st.IdSyllabus);
      #endregion

      #region History Syllabus
      modelBuilder.Entity<HistorySyllabus>()
          .HasKey("IdUser", "IdSyllabus", "ModifiedOn");

      modelBuilder.Entity<HistorySyllabus>()
          .HasOne<User>(hs => hs.User)
          .WithMany(u => u.HistorySyllabi)
          .HasForeignKey(hs => hs.IdUser);

      modelBuilder.Entity<HistorySyllabus>()
          .HasOne<Syllabus>(hs => hs.Syllabus)
          .WithMany(u => u.HistorySyllabi)
          .HasForeignKey(hs => hs.IdSyllabus);

      modelBuilder.Entity<HistorySyllabus>()
          .Property("ModifiedOn")
          .HasColumnType("datetime")
          .IsRequired();

      modelBuilder.Entity<HistorySyllabus>()
          .Property("Action")
          .HasColumnType("varchar(50)")
          .IsRequired();
      #endregion

      #region AssigmentSchema
      modelBuilder.Entity<AssignmentSchema>()
          .HasKey("IDSyllabus");

      modelBuilder.Entity<AssignmentSchema>()
          .HasOne<Syllabus>(ass => ass.Syllabus)
          .WithOne(s => s.AssignmentSchema)
          .HasForeignKey<AssignmentSchema>(ass => ass.IDSyllabus);

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PercentQuiz")
          .HasColumnType("float");

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PercentAssigment")
          .HasColumnType("float");

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PercentFinal")
          .HasColumnType("float");

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PercentTheory")
          .HasColumnType("float");

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PercentFinalPractice")
          .HasColumnType("float");

      modelBuilder.Entity<AssignmentSchema>()
          .Property("PassingCriterial")
          .HasColumnType("float");
      #endregion

      #region Session
      modelBuilder.Entity<Session>()
          .HasKey("Id");

      modelBuilder.Entity<Session>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Session>()
          .Property("Index")
          .HasColumnType("int")
          .IsRequired();

      modelBuilder.Entity<Session>()
          .Property("Status")
          .HasColumnType("int")
          .IsRequired(false);

      modelBuilder.Entity<Session>()
    .HasOne<Syllabus>(s => s.Syllabus)
    .WithMany(sl => sl.Sessions)
    .HasForeignKey(s => s.IdSyllabus);

      #endregion

      #region Unit
      modelBuilder.Entity<Unit>()
          .HasKey("Id");

      modelBuilder.Entity<Unit>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<Unit>()
          .Property("Index")
          .HasColumnType("int")
          .IsRequired();
      modelBuilder.Entity<Unit>()
          .Property("Status")
          .HasColumnType("int")
          .IsRequired(false);

      modelBuilder.Entity<Unit>()
    .HasOne<Session>(u => u.Session)
    .WithMany(s => s.Units)
    .HasForeignKey(u => u.IdSession);
      #endregion

      #region Curriculum
      modelBuilder.Entity<Curriculum>()
          .HasKey("IdProgram", "IdSyllabus");

      modelBuilder.Entity<Curriculum>()
          .Property("NumberOrder")
          .IsRequired();

      modelBuilder.Entity<Curriculum>()
          .HasOne<TrainingProgram>(c => c.TrainingProgram)
          .WithMany(tp => tp.Curricula)
          .HasForeignKey(c => c.IdProgram);

      modelBuilder.Entity<Curriculum>()
          .HasOne<Syllabus>(c => c.Syllabus)
          .WithMany(s => s.Curricula)
          .HasForeignKey(c => c.IdSyllabus);
      #endregion

      #region History Training Program
      modelBuilder.Entity<HistoryTrainingProgram>()
          .HasKey("IdUser", "IdProgram");

      modelBuilder.Entity<HistoryTrainingProgram>()
          .Property("ModifiedOn")
          .HasColumnType("date")
          .IsRequired();

      modelBuilder.Entity<HistoryTrainingProgram>()
          .HasOne<User>(htp => htp.User)
          .WithMany(u => u.HistoryTrainingPrograms)
          .HasForeignKey(htp => htp.IdUser);

      modelBuilder.Entity<HistoryTrainingProgram>()
          .HasOne<TrainingProgram>(htp => htp.TrainingProgram)
          .WithMany(tp => tp.HistoryTrainingPrograms)
          .HasForeignKey(htp => htp.IdProgram);
      #endregion

      #region Class
      modelBuilder.Entity<Class>()
          .HasKey("Id");

      modelBuilder.Entity<Class>()
          .Property("ClassCode")
          .HasColumnType("varchar")
          .HasMaxLength(50)
          .IsRequired(false);


      modelBuilder.Entity<Class>()
      .Property("Name")
      .HasColumnType("varchar")
      .HasMaxLength(50);

      modelBuilder.Entity<Class>()
        .Property("StartTimeLearning");
      // .HasColumnType("nvarchar")
      // .HasMaxLength(50);

      modelBuilder.Entity<Class>()
      .Property("EndTimeLearing");
      //   .HasColumnType("nvarchar")
      //   .HasMaxLength(50);

      modelBuilder.Entity<Class>()
          .Property("ReviewedOn")
          .HasColumnType("date");

      modelBuilder.Entity<Class>()
          .Property("CreatedOn")
          .HasColumnType("date");

      modelBuilder.Entity<Class>()
          .Property("ApprovedOn")
          .HasColumnType("date");

      modelBuilder.Entity<Class>()
          .Property("StartDate")
          .HasColumnType("date");

      modelBuilder.Entity<Class>()
          .Property("EndDate")
          .HasColumnType("date");

      modelBuilder.Entity<Class>()
          .HasOne<User>(c => c.ReviewedUser)
          .WithMany(u => u.ReviewedClasses)
          .HasForeignKey(c => c.ReviewedBy);

      modelBuilder.Entity<Class>()
          .HasOne<User>(c => c.CreatedUser)
          .WithMany(u => u.CreatedClasses)
          .HasForeignKey(c => c.CreatedBy);

      modelBuilder.Entity<Class>()
          .HasOne<User>(c => c.ApprovedUser)
          .WithMany(u => u.ApprovedClasses)
          .HasForeignKey(c => c.ApprovedBy);

      modelBuilder.Entity<Class>()
          .HasOne<TrainingProgram>(c => c.TrainingProgram)
          .WithMany(tp => tp.Classes)
          .HasForeignKey(c => c.IdProgram);

      modelBuilder.Entity<Class>()
          .HasOne<ClassTechnicalGroup>(c => c.classTechnicalGroup)
          .WithMany(ctg => ctg.Classes)
          .HasForeignKey(c => c.IdTechnicalGroup);

      modelBuilder.Entity<Class>()
          .HasOne<FsoftUnit>(c => c.FsoftUnit)
          .WithMany(fu => fu.Classes)
          .HasForeignKey(c => c.IdFSU);

      modelBuilder.Entity<Class>()
          .HasOne<FSUContactPoint>(c => c.FSUContactPoint)
          .WithMany(fcp => fcp.Classes)
          .HasForeignKey(c => c.IdFSUContact);

      modelBuilder.Entity<Class>()
          .HasOne<ClassStatus>(c => c.ClassStatus)
          .WithMany(cs => cs.Classes)
          .HasForeignKey(c => c.IdStatus);

      modelBuilder.Entity<Class>()
          .HasOne<ClassSite>(c => c.ClassSite)
          .WithMany(cs => cs.Classes)
          .HasForeignKey(c => c.IdSite);

      modelBuilder.Entity<Class>()
          .HasOne<ClassUniversityCode>(c => c.ClassUniversityCode)
          .WithMany(cuc => cuc.Classes)
          .HasForeignKey(c => c.IdUniversity);

      modelBuilder.Entity<Class>()
          .HasOne<ClassFormatType>(c => c.ClassFormatType)
          .WithMany(cft => cft.Classes)
          .HasForeignKey(c => c.IdFormatType);

      modelBuilder.Entity<Class>()
          .HasOne<ClassProgramCode>(c => c.ClassProgramCode)
          .WithMany(cpc => cpc.Classes)
          .HasForeignKey(c => c.IdProgramContent);

      modelBuilder.Entity<Class>()
          .HasOne<AttendeeType>(c => c.AttendeeType)
          .WithMany(at => at.Classes)
          .HasForeignKey(c => c.IdAttendeeType);



      #endregion

      #region Attendee Type
      modelBuilder.Entity<AttendeeType>()
          .HasKey("Id");

      modelBuilder.Entity<AttendeeType>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Class Format Type
      modelBuilder.Entity<ClassFormatType>()
          .HasKey("Id");

      modelBuilder.Entity<ClassFormatType>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Class Program Code
      modelBuilder.Entity<ClassProgramCode>()
          .HasKey("Id");

      modelBuilder.Entity<ClassProgramCode>()
          .Property("ProgramCode")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Class Site
      modelBuilder.Entity<ClassSite>()
          .HasKey("Id");

      modelBuilder.Entity<ClassSite>()
          .Property("Site")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Class Status
      modelBuilder.Entity<ClassStatus>()
          .HasKey("Id");

      modelBuilder.Entity<ClassStatus>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Class Technical Group
      modelBuilder.Entity<ClassTechnicalGroup>()
          .HasKey("Id");

      modelBuilder.Entity<ClassTechnicalGroup>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(100);
      #endregion

      #region Class University Code
      modelBuilder.Entity<ClassUniversityCode>()
          .HasKey("Id");

      modelBuilder.Entity<ClassUniversityCode>()
          .Property("UniversityCode")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);
      #endregion

      #region Fsoft Unit
      modelBuilder.Entity<FsoftUnit>()
          .HasKey("Id");

      modelBuilder.Entity<FsoftUnit>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50);

      modelBuilder.Entity<FsoftUnit>()
          .Property("Status")
          .IsRequired();
      #endregion

      #region FSU Contact Point
      modelBuilder.Entity<FSUContactPoint>()
          .HasKey("Id");

      modelBuilder.Entity<FSUContactPoint>()
          .Property("Contact")
          .HasColumnType("nvarchar")
          .HasMaxLength(100);

      modelBuilder.Entity<FSUContactPoint>()
          .Property("Status")
          .IsRequired();

      modelBuilder.Entity<FSUContactPoint>()
          .HasOne<FsoftUnit>(fcp => fcp.FSU)
          .WithMany(fu => fu.FSUContactPoints)
          .HasForeignKey(fcp => fcp.IdFSU);
      #endregion

      #region location
      modelBuilder.Entity<Location>()
          .HasKey("Id");

      modelBuilder.Entity<Location>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(100);

      modelBuilder.Entity<Location>()
          .Property("Status")
          .IsRequired();
      #endregion

      #region Class Location
      modelBuilder.Entity<ClassLocation>()
          .HasKey("IdClass", "IdLocation");

      modelBuilder.Entity<ClassLocation>()
          .HasOne<Class>(cl => cl.Class)
          .WithMany(c => c.Locations)
          .HasForeignKey(cl => cl.IdClass);

      modelBuilder.Entity<ClassLocation>()
          .HasOne<Location>(cl => cl.Location)
          .WithMany(l => l.Locations)
          .HasForeignKey(cl => cl.IdLocation);
      #endregion

      #region Class Selected Date
      modelBuilder.Entity<ClassSelectedDate>()
          .HasKey("Id");

      modelBuilder.Entity<ClassSelectedDate>()
          .Property("Status")
          .IsRequired();

      modelBuilder.Entity<ClassSelectedDate>()
          .Property("ActiveDate")
          .HasColumnType("date")
          .IsRequired();

      modelBuilder.Entity<ClassSelectedDate>()
          .HasOne<Class>(csd => csd.Class)
          .WithMany(c => c.ClassSelectedDates)
          .HasForeignKey(csd => csd.IdClass);
      #endregion

      #region Class Update History
      modelBuilder.Entity<ClassUpdateHistory>()
          .HasKey("IdClass", "ModifyBy", "UpdateDate");

      modelBuilder.Entity<ClassUpdateHistory>()
          .Property("UpdateDate")
          .IsRequired();

      modelBuilder.Entity<ClassUpdateHistory>()
          .HasOne<Class>(cuh => cuh.Class)
          .WithMany(c => c.ClassUpdateHistories)
          .HasForeignKey(cuh => cuh.IdClass);

      modelBuilder.Entity<ClassUpdateHistory>()
          .HasOne<User>(cuh => cuh.User)
          .WithMany(u => u.ClassUpdateHistories)
          .HasForeignKey(cuh => cuh.ModifyBy)
          .OnDelete(DeleteBehavior.Restrict);
      #endregion


      #region Class Trainee
      modelBuilder.Entity<ClassTrainee>()
          .HasKey("IdUser", "IdClass");

      modelBuilder.Entity<ClassTrainee>()
          .HasOne<Class>(ct => ct.Class)
          .WithMany(c => c.ClassTrainees)
          .HasForeignKey(ct => ct.IdClass);

      modelBuilder.Entity<ClassTrainee>()
          .HasOne<User>(ct => ct.User)
          .WithMany(u => u.ClassTrainees)
          .HasForeignKey(ct => ct.IdUser)
          .OnDelete(DeleteBehavior.Restrict);
      #endregion

      #region Class Admin
      modelBuilder.Entity<ClassAdmin>()
          .HasKey("IdUser", "IdClass");

      modelBuilder.Entity<ClassAdmin>()
          .HasOne<User>(ca => ca.User)
          .WithMany(u => u.ClassAdmins)
          .HasForeignKey(ca => ca.IdUser)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<ClassAdmin>()
          .HasOne<Class>(ca => ca.Class)
          .WithMany(c => c.ClassAdmins)
          .HasForeignKey(ca => ca.IdClass);

      #endregion

      #region Class Mentor
      modelBuilder.Entity<ClassMentor>()
          .HasKey("IdUser", "IdClass");

      modelBuilder.Entity<ClassMentor>()
          .HasOne<Class>(cm => cm.Class)
          .WithMany(c => c.ClassMentors)
          .HasForeignKey(cm => cm.IdClass);

      modelBuilder.Entity<ClassMentor>()
          .HasOne<User>(cm => cm.User)
          .WithMany(u => u.ClassMentors)
          .HasForeignKey(cm => cm.IdUser)
          .OnDelete(DeleteBehavior.Restrict);
      #endregion

      #region training program
      modelBuilder.Entity<TrainingProgram>()
          .HasKey("Id");

      modelBuilder.Entity<TrainingProgram>()
          .Property("Name")
          .HasColumnType("nvarchar")
          .HasMaxLength(50)
          .IsRequired();

      modelBuilder.Entity<TrainingProgram>()
          .Property("Status")
          .IsRequired();
      #endregion

      #region Seed Data
      modelBuilder.SeedDeliveryType();
      modelBuilder.SeedLessonFormatType();
      modelBuilder.SeedLevel();
      modelBuilder.SeedOutputStandard();
      modelBuilder.SeedPermission();
      modelBuilder.SeedRole();
      modelBuilder.SeedPermissionRight();
      modelBuilder.SeedRight();
      modelBuilder.SeedUser();
      // --------------Class-----------------
      modelBuilder.SeedClassTechnicalGroup();
      modelBuilder.SeedClassProgramCode();
      modelBuilder.SeedAttendeeType();
      modelBuilder.SeedClassFormatType();
      modelBuilder.SeedFsoftunit();
      modelBuilder.SeedFsuContactPoint();
      modelBuilder.SeedLocation();
      modelBuilder.SeedClassSite();
      modelBuilder.SeedClassUniversityCode();
      modelBuilder.SeedClassStatus();
      modelBuilder.SeedTrainingProgram();
      modelBuilder.SeedTrainingProgramHistory();
      modelBuilder.SeedClass();
      modelBuilder.SeedClassHistory();
      //-------------------------------------
      #endregion

    }
  }
}
