using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendeeTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassFormatTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFormatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassProgramCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassProgramCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassSites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Site = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassTechnicalGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTechnicalGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassUniversityCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUniversityCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormatTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FsoftUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FsoftUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutputStandards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputStandards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rights",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FSUContactPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFSU = table.Column<long>(type: "bigint", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FSUContactPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FSUContactPoints_FsoftUnits_IdFSU",
                        column: x => x.IdFSU,
                        principalTable: "FsoftUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Syllabi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AttendeeNumber = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<double>(type: "float", nullable: false),
                    Technicalrequirement = table.Column<string>(type: "NText", nullable: true),
                    CourseObjectives = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TrainingPrinciple = table.Column<string>(type: "NText", nullable: true),
                    Description = table.Column<string>(type: "NText", nullable: true),
                    HyperLink = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    IdLevel = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syllabi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Syllabi_Levels_IdLevel",
                        column: x => x.IdLevel,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRights",
                columns: table => new
                {
                    IdRight = table.Column<long>(type: "bigint", nullable: false),
                    IdRole = table.Column<long>(type: "bigint", nullable: false),
                    IdPermission = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRights", x => new { x.IdRight, x.IdRole });
                    table.ForeignKey(
                        name: "FK_PermissionRights_Permissions_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRights_Rights_IdRight",
                        column: x => x.IdRight,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRights_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRights",
                columns: table => new
                {
                    IDRight = table.Column<long>(type: "bigint", nullable: false),
                    IDRole = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRights", x => new { x.IDRight, x.IDRole });
                    table.ForeignKey(
                        name: "FK_RoleRights_Rights_IDRight",
                        column: x => x.IDRight,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleRights_Roles_IDRole",
                        column: x => x.IDRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "char(1)", nullable: false),
                    Phone = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResetPasswordOtp = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    IdRole = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignmentSchemas",
                columns: table => new
                {
                    IDSyllabus = table.Column<long>(type: "bigint", nullable: false),
                    PercentQuiz = table.Column<double>(type: "float", nullable: true),
                    PercentAssigment = table.Column<double>(type: "float", nullable: true),
                    PercentFinal = table.Column<double>(type: "float", nullable: true),
                    PercentTheory = table.Column<double>(type: "float", nullable: true),
                    PercentFinalPractice = table.Column<double>(type: "float", nullable: true),
                    PassingCriterial = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignmentSchemas", x => x.IDSyllabus);
                    table.ForeignKey(
                        name: "FK_assignmentSchemas_Syllabi_IDSyllabus",
                        column: x => x.IDSyllabus,
                        principalTable: "Syllabi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curricula",
                columns: table => new
                {
                    IdProgram = table.Column<long>(type: "bigint", nullable: false),
                    IdSyllabus = table.Column<long>(type: "bigint", nullable: false),
                    NumberOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curricula", x => new { x.IdProgram, x.IdSyllabus });
                    table.ForeignKey(
                        name: "FK_Curricula_Syllabi_IdSyllabus",
                        column: x => x.IdSyllabus,
                        principalTable: "Syllabi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curricula_TrainingPrograms_IdProgram",
                        column: x => x.IdProgram,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IdSyllabus = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Syllabi_IdSyllabus",
                        column: x => x.IdSyllabus,
                        principalTable: "Syllabi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ClassCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTimeLearning = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTimeLearing = table.Column<TimeSpan>(type: "time", nullable: true),
                    ReviewedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReviewedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    ApprovedBy = table.Column<long>(type: "bigint", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "date", nullable: true),
                    PlannedAtendee = table.Column<int>(type: "int", nullable: true),
                    ActualAttendee = table.Column<int>(type: "int", nullable: true),
                    AcceptedAttendee = table.Column<int>(type: "int", nullable: true),
                    CurrentSession = table.Column<int>(type: "int", nullable: true),
                    CurrentUnit = table.Column<int>(type: "int", nullable: true),
                    StartYear = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    ClassNumber = table.Column<int>(type: "int", nullable: false),
                    IdProgram = table.Column<long>(type: "bigint", nullable: true),
                    IdTechnicalGroup = table.Column<long>(type: "bigint", nullable: true),
                    IdFSU = table.Column<long>(type: "bigint", nullable: true),
                    IdFSUContact = table.Column<long>(type: "bigint", nullable: true),
                    IdStatus = table.Column<long>(type: "bigint", nullable: false),
                    IdSite = table.Column<long>(type: "bigint", nullable: true),
                    IdUniversity = table.Column<long>(type: "bigint", nullable: true),
                    IdFormatType = table.Column<long>(type: "bigint", nullable: true),
                    IdProgramContent = table.Column<long>(type: "bigint", nullable: true),
                    IdAttendeeType = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_AttendeeTypes_IdAttendeeType",
                        column: x => x.IdAttendeeType,
                        principalTable: "AttendeeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_ClassFormatTypes_IdFormatType",
                        column: x => x.IdFormatType,
                        principalTable: "ClassFormatTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_ClassProgramCodes_IdProgramContent",
                        column: x => x.IdProgramContent,
                        principalTable: "ClassProgramCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_ClassSites_IdSite",
                        column: x => x.IdSite,
                        principalTable: "ClassSites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_ClassStatuses_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "ClassStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_ClassTechnicalGroups_IdTechnicalGroup",
                        column: x => x.IdTechnicalGroup,
                        principalTable: "ClassTechnicalGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_ClassUniversityCodes_IdUniversity",
                        column: x => x.IdUniversity,
                        principalTable: "ClassUniversityCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_FsoftUnits_IdFSU",
                        column: x => x.IdFSU,
                        principalTable: "FsoftUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_FSUContactPoints_IdFSUContact",
                        column: x => x.IdFSUContact,
                        principalTable: "FSUContactPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_TrainingPrograms_IdProgram",
                        column: x => x.IdProgram,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Classes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HistorySyllabi",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdSyllabus = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Action = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySyllabi", x => new { x.IdUser, x.IdSyllabus, x.ModifiedOn });
                    table.ForeignKey(
                        name: "FK_HistorySyllabi_Syllabi_IdSyllabus",
                        column: x => x.IdSyllabus,
                        principalTable: "Syllabi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorySyllabi_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTrainingPrograms",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdProgram = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTrainingPrograms", x => new { x.IdUser, x.IdProgram });
                    table.ForeignKey(
                        name: "FK_HistoryTrainingPrograms_TrainingPrograms_IdProgram",
                        column: x => x.IdProgram,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryTrainingPrograms_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusTrainers",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdSyllabus = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusTrainers", x => new { x.IdUser, x.IdSyllabus });
                    table.ForeignKey(
                        name: "FK_SyllabusTrainers_Syllabi_IdSyllabus",
                        column: x => x.IdSyllabus,
                        principalTable: "Syllabi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SyllabusTrainers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IdSession = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Sessions_IdSession",
                        column: x => x.IdSession,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassAdmins",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdClass = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAdmins", x => new { x.IdUser, x.IdClass });
                    table.ForeignKey(
                        name: "FK_ClassAdmins_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassAdmins_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassLocations",
                columns: table => new
                {
                    IdClass = table.Column<long>(type: "bigint", nullable: false),
                    IdLocation = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLocations", x => new { x.IdClass, x.IdLocation });
                    table.ForeignKey(
                        name: "FK_ClassLocations_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLocations_Locations_IdLocation",
                        column: x => x.IdLocation,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "classMentors",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdClass = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classMentors", x => new { x.IdUser, x.IdClass });
                    table.ForeignKey(
                        name: "FK_classMentors_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_classMentors_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassSelectedDates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClass = table.Column<long>(type: "bigint", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSelectedDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSelectedDates_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTrainees",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdClass = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTrainees", x => new { x.IdUser, x.IdClass });
                    table.ForeignKey(
                        name: "FK_ClassTrainees_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTrainees_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassUpdateHistories",
                columns: table => new
                {
                    IdClass = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUpdateHistories", x => new { x.IdClass, x.ModifyBy, x.UpdateDate });
                    table.ForeignKey(
                        name: "FK_ClassUpdateHistories_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassUpdateHistories_Users_ModifyBy",
                        column: x => x.ModifyBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IdDeliveryType = table.Column<long>(type: "bigint", nullable: false),
                    IdFormatType = table.Column<long>(type: "bigint", nullable: false),
                    IdOutputStandard = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IdUnit = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_DeliveryTypes_IdDeliveryType",
                        column: x => x.IdDeliveryType,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_FormatTypes_IdFormatType",
                        column: x => x.IdFormatType,
                        principalTable: "FormatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_OutputStandards_IdOutputStandard",
                        column: x => x.IdOutputStandard,
                        principalTable: "OutputStandards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HyperLink = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    IdLesson = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Lessons_IdLesson",
                        column: x => x.IdLesson,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryMaterials",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdMaterial = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Action = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryMaterials", x => new { x.IdUser, x.IdMaterial, x.ModifiedOn });
                    table.ForeignKey(
                        name: "FK_HistoryMaterials_Materials_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryMaterials_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AttendeeTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "FRF" },
                    { 2L, "FR" },
                    { 3L, "CPL" },
                    { 4L, "PFR" },
                    { 5L, "CPLU" }
                });

            migrationBuilder.InsertData(
                table: "ClassFormatTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Offline" },
                    { 2L, "Online" },
                    { 3L, "OJT" },
                    { 4L, "Virtual Training" },
                    { 5L, "Blended" }
                });

            migrationBuilder.InsertData(
                table: "ClassProgramCodes",
                columns: new[] { "Id", "ProgramCode" },
                values: new object[,]
                {
                    { 1L, "JAVA" },
                    { 2L, "NET" },
                    { 3L, "FE" },
                    { 4L, "Android" },
                    { 5L, "CPP" },
                    { 6L, "Angular" },
                    { 7L, "REACT" },
                    { 8L, "PRN" },
                    { 9L, "EMB" },
                    { 10L, "OST" },
                    { 11L, "SP" },
                    { 12L, "TEST" },
                    { 13L, "IOS" },
                    { 14L, "COBOL" },
                    { 15L, "AUT" },
                    { 16L, "AI" },
                    { 17L, "DE" },
                    { 18L, "QA" },
                    { 19L, "COMTOR" },
                    { 20L, "DevOps" },
                    { 21L, "SAP" },
                    { 22L, "AC" },
                    { 23L, "TC" },
                    { 24L, "GOL" },
                    { 25L, "Flutter" },
                    { 26L, "ServiceNow" },
                    { 27L, "PFR_JAVA" },
                    { 28L, "FJW" },
                    { 29L, "JWD" },
                    { 30L, "JSE" },
                    { 31L, "PAD" },
                    { 32L, "FED" }
                });

            migrationBuilder.InsertData(
                table: "ClassProgramCodes",
                columns: new[] { "Id", "ProgramCode" },
                values: new object[,]
                {
                    { 33L, "FNW" },
                    { 34L, "NWD" },
                    { 35L, "NPD" },
                    { 36L, "WAT" },
                    { 37L, "PRD" },
                    { 38L, "PML" },
                    { 39L, "ITF" },
                    { 40L, "FJB" },
                    { 41L, "OCA" },
                    { 42L, "BA" },
                    { 43L, "APM" },
                    { 44L, "DSA" },
                    { 45L, "FIF" },
                    { 46L, "DEE" },
                    { 47L, "STE" },
                    { 48L, "Flexcube" },
                    { 49L, "OCP" },
                    { 50L, "FUJS" },
                    { 51L, "CES" },
                    { 52L, "CLOUD" },
                    { 53L, "PHP" },
                    { 54L, "NodeJS" },
                    { 55L, "ASE" },
                    { 56L, "MPP" },
                    { 57L, "DATA" },
                    { 58L, "Sitecore" },
                    { 59L, "MAT" },
                    { 60L, "AND" },
                    { 61L, "ADR" },
                    { 62L, "JAVA" },
                    { 63L, "Mobile" },
                    { 64L, "GST_JAVA" },
                    { 65L, "LITE_CPP" },
                    { 66L, "WinApp" },
                    { 67L, "Magento" },
                    { 68L, "Python" },
                    { 69L, "RN" },
                    { 70L, "FUKS" },
                    { 71L, "RPA" },
                    { 72L, "Erlang" },
                    { 73L, "Golang" },
                    { 74L, "C++/Linux" }
                });

            migrationBuilder.InsertData(
                table: "ClassProgramCodes",
                columns: new[] { "Id", "ProgramCode" },
                values: new object[,]
                {
                    { 75L, "AEM" },
                    { 76L, "GST_EMB" },
                    { 77L, "GST_NET" },
                    { 78L, "JP" }
                });

            migrationBuilder.InsertData(
                table: "ClassSites",
                columns: new[] { "Id", "Site" },
                values: new object[,]
                {
                    { 1L, "HN" },
                    { 2L, "HCM" },
                    { 3L, "DN" },
                    { 4L, "CT" },
                    { 5L, "QN" }
                });

            migrationBuilder.InsertData(
                table: "ClassStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Draft" },
                    { 2L, "Reviewing" },
                    { 3L, "Approving" },
                    { 4L, "Start" },
                    { 5L, "Done" },
                    { 6L, "Delayed" },
                    { 7L, "Opened" },
                    { 8L, "Active" },
                    { 9L, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "ClassTechnicalGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Java" },
                    { 2L, ".NET" },
                    { 3L, "FE" },
                    { 4L, "Android" },
                    { 5L, "CPP" },
                    { 6L, "Angular" },
                    { 7L, "React" },
                    { 8L, "Embedded" },
                    { 9L, "Out System" },
                    { 10L, "Sharepoint" },
                    { 11L, "iOS" },
                    { 12L, "Cobol" },
                    { 13L, "AUT" },
                    { 14L, "AI" },
                    { 15L, "Data" },
                    { 16L, "QA" },
                    { 17L, "Comtor" },
                    { 18L, "DevOps" },
                    { 19L, "SAP" },
                    { 20L, "ABAP" },
                    { 21L, "Go Lang" },
                    { 22L, "Flutter" },
                    { 23L, "ServiceNow" },
                    { 24L, "Front-End" }
                });

            migrationBuilder.InsertData(
                table: "ClassTechnicalGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 25L, "Manual Test" },
                    { 26L, "Automation Test" },
                    { 27L, "C++" },
                    { 28L, "Python" },
                    { 29L, "IT" },
                    { 30L, "OCA8" },
                    { 31L, "BA" },
                    { 32L, "APM" },
                    { 33L, "DSA" },
                    { 34L, "FIF" },
                    { 35L, "STE" },
                    { 36L, "Flexcube" },
                    { 37L, "CLOUD" },
                    { 38L, "PHP" },
                    { 39L, "NodeJS" },
                    { 40L, "Security Engineer" },
                    { 41L, "Microsoft Power Platform" },
                    { 42L, "Data Engineer" },
                    { 43L, "Sitecore" },
                    { 44L, "Agile" },
                    { 45L, "React Native" },
                    { 46L, "Certificate" },
                    { 47L, "SAP,ABAP" },
                    { 48L, "Mobile" },
                    { 49L, "WinApp" },
                    { 50L, "PHP" },
                    { 51L, "RPA" },
                    { 52L, "Erlang" },
                    { 53L, "Fullstack Java" },
                    { 54L, "Fullstack .NET" },
                    { 55L, "Java Standard" },
                    { 56L, ".NET standard" },
                    { 57L, "Golang" },
                    { 58L, "C++/Linux" },
                    { 59L, "AEM" },
                    { 60L, "JP" }
                });

            migrationBuilder.InsertData(
                table: "ClassUniversityCodes",
                columns: new[] { "Id", "UniversityCode" },
                values: new object[] { 1L, "ALL" });

            migrationBuilder.InsertData(
                table: "DeliveryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Assignment/Lab" },
                    { 2L, "Concept/Lecture" },
                    { 3L, "Guide/Review" },
                    { 4L, "Test/Quiz" },
                    { 5L, "Exam" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6L, "Seminar/Workshop" });

            migrationBuilder.InsertData(
                table: "FormatTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Offline" },
                    { 2L, "Online" }
                });

            migrationBuilder.InsertData(
                table: "FsoftUnits",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1L, "FHM", 1 },
                    { 2L, "FU", 1 },
                    { 3L, "FPTN", 1 }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "All level" },
                    { 2L, "Fresher" },
                    { 3L, "Intern" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1L, "FTown 1", 0 },
                    { 2L, "FTown 2", 0 },
                    { 3L, "FTown 3", 0 }
                });

            migrationBuilder.InsertData(
                table: "OutputStandards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "H4SD" },
                    { 2L, "K6SD" },
                    { 3L, "H6SD" },
                    { 4L, "H1ST" },
                    { 5L, "H2SD" },
                    { 6L, "K4SD" },
                    { 7L, "K3SD " }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Access denied" },
                    { 2L, "View" },
                    { 3L, "Modify" },
                    { 4L, "Create" },
                    { 5L, "Full access" }
                });

            migrationBuilder.InsertData(
                table: "Rights",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Syllabus" },
                    { 2L, "Training program" },
                    { 3L, "Class" },
                    { 4L, "Learning material" },
                    { 5L, "User" },
                    { 6L, "Training calendar" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Super Admin" },
                    { 2L, "Class Admin" },
                    { 3L, "Trainer" },
                    { 4L, "Student" }
                });

            migrationBuilder.InsertData(
                table: "FSUContactPoints",
                columns: new[] { "Id", "Contact", "IdFSU", "Status" },
                values: new object[,]
                {
                    { 1L, "BaCH@fsoft.com.vn", 1L, 1 },
                    { 2L, "0912345678", 2L, 1 }
                });

            migrationBuilder.InsertData(
                table: "PermissionRights",
                columns: new[] { "IdRight", "IdRole", "IdPermission" },
                values: new object[,]
                {
                    { 1L, 1L, 5L },
                    { 2L, 1L, 5L },
                    { 3L, 1L, 5L },
                    { 4L, 1L, 5L },
                    { 5L, 1L, 5L },
                    { 6L, 1L, 5L }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Address", "DateOfBirth", "Email", "FullName", "Gender", "IdRole", "Image", "Password", "Phone", "ResetPasswordOtp", "Status", "UserName" },
                values: new object[,]
                {
                    { 1L, "123 đường 456", new DateTime(2000, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@fsoft.com", "Super Admin", "M", 1L, null, "$2a$11$k7zKp9cQOIE3/c22YdD29O52l8x.9bbji4kJOPJ3Jy.f4kIUYIQ0G", "0123456789", null, 1, "superadmin@fsoft.com" },
                    { 2L, "123 đường 456", new DateTime(2000, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "classadmin@fsoft.com", "Class Admin", "F", 2L, null, "$2a$11$IWL97xH2L60fhHMoo38msOYC7ZsP6GsrpnO.CLS04IRNBkqs8TdWS", "0123456789", null, 1, "classadmin@fsoft.com" },
                    { 3L, "123 đường 456", new DateTime(2000, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer@fsoft.com", "Trainer", "M", 3L, null, "$2a$11$4/mkPNwz0l/.e7zXfRT69eKsP327tqz10Ldf5s0iWAZLNCWRRRrxK", "0123456789", null, 1, "trainer@fsoft.com" },
                    { 4L, "123 đường 456", new DateTime(2000, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "student@fsoft.com", "Student", "M", 4L, null, "$2a$11$WTeAE4MdAkR4ZtVooCdZkuuzam5sdxDTpm1VJqL/RFIEJNLJk.PX2", "0123456789", null, 1, "student@fsoft.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassAdmins_IdClass",
                table: "ClassAdmins",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ApprovedBy",
                table: "Classes",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CreatedBy",
                table: "Classes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdAttendeeType",
                table: "Classes",
                column: "IdAttendeeType");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdFormatType",
                table: "Classes",
                column: "IdFormatType");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdFSU",
                table: "Classes",
                column: "IdFSU");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdFSUContact",
                table: "Classes",
                column: "IdFSUContact");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdProgram",
                table: "Classes",
                column: "IdProgram");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdProgramContent",
                table: "Classes",
                column: "IdProgramContent");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdSite",
                table: "Classes",
                column: "IdSite");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdStatus",
                table: "Classes",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdTechnicalGroup",
                table: "Classes",
                column: "IdTechnicalGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdUniversity",
                table: "Classes",
                column: "IdUniversity");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ReviewedBy",
                table: "Classes",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLocations_IdLocation",
                table: "ClassLocations",
                column: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_classMentors_IdClass",
                table: "classMentors",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSelectedDates_IdClass",
                table: "ClassSelectedDates",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTrainees_IdClass",
                table: "ClassTrainees",
                column: "IdClass");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUpdateHistories_ModifyBy",
                table: "ClassUpdateHistories",
                column: "ModifyBy");

            migrationBuilder.CreateIndex(
                name: "IX_Curricula_IdSyllabus",
                table: "Curricula",
                column: "IdSyllabus");

            migrationBuilder.CreateIndex(
                name: "IX_FSUContactPoints_IdFSU",
                table: "FSUContactPoints",
                column: "IdFSU");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryMaterials_IdMaterial",
                table: "HistoryMaterials",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_HistorySyllabi_IdSyllabus",
                table: "HistorySyllabi",
                column: "IdSyllabus");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTrainingPrograms_IdProgram",
                table: "HistoryTrainingPrograms",
                column: "IdProgram");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdDeliveryType",
                table: "Lessons",
                column: "IdDeliveryType");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdFormatType",
                table: "Lessons",
                column: "IdFormatType");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdOutputStandard",
                table: "Lessons",
                column: "IdOutputStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdUnit",
                table: "Lessons",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_IdLesson",
                table: "Materials",
                column: "IdLesson");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRights_IdPermission",
                table: "PermissionRights",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRights_IdRole",
                table: "PermissionRights",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRights_IDRole",
                table: "RoleRights",
                column: "IDRole");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdSyllabus",
                table: "Sessions",
                column: "IdSyllabus");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabi_IdLevel",
                table: "Syllabi",
                column: "IdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusTrainers_IdSyllabus",
                table: "SyllabusTrainers",
                column: "IdSyllabus");

            migrationBuilder.CreateIndex(
                name: "IX_Units_IdSession",
                table: "Units",
                column: "IdSession");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignmentSchemas");

            migrationBuilder.DropTable(
                name: "ClassAdmins");

            migrationBuilder.DropTable(
                name: "ClassLocations");

            migrationBuilder.DropTable(
                name: "classMentors");

            migrationBuilder.DropTable(
                name: "ClassSelectedDates");

            migrationBuilder.DropTable(
                name: "ClassTrainees");

            migrationBuilder.DropTable(
                name: "ClassUpdateHistories");

            migrationBuilder.DropTable(
                name: "Curricula");

            migrationBuilder.DropTable(
                name: "HistoryMaterials");

            migrationBuilder.DropTable(
                name: "HistorySyllabi");

            migrationBuilder.DropTable(
                name: "HistoryTrainingPrograms");

            migrationBuilder.DropTable(
                name: "PermissionRights");

            migrationBuilder.DropTable(
                name: "RoleRights");

            migrationBuilder.DropTable(
                name: "SyllabusTrainers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Rights");

            migrationBuilder.DropTable(
                name: "AttendeeTypes");

            migrationBuilder.DropTable(
                name: "ClassFormatTypes");

            migrationBuilder.DropTable(
                name: "ClassProgramCodes");

            migrationBuilder.DropTable(
                name: "ClassSites");

            migrationBuilder.DropTable(
                name: "ClassStatuses");

            migrationBuilder.DropTable(
                name: "ClassTechnicalGroups");

            migrationBuilder.DropTable(
                name: "ClassUniversityCodes");

            migrationBuilder.DropTable(
                name: "FSUContactPoints");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "FsoftUnits");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "FormatTypes");

            migrationBuilder.DropTable(
                name: "OutputStandards");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Syllabi");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
