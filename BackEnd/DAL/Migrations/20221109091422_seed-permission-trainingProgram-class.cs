using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class seedpermissiontrainingProgramclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6L, "Delete while viewing" });

            migrationBuilder.InsertData(
                table: "TrainingPrograms",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[] { 1L, "C# Foundation", 1 });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "AcceptedAttendee", "ActualAttendee", "ApprovedBy", "ApprovedOn", "ClassCode", "ClassNumber", "CreatedBy", "CreatedOn", "CurrentSession", "CurrentUnit", "EndDate", "EndTimeLearing", "IdAttendeeType", "IdFSU", "IdFSUContact", "IdFormatType", "IdProgram", "IdProgramContent", "IdSite", "IdStatus", "IdTechnicalGroup", "IdUniversity", "Name", "PlannedAtendee", "ReviewedBy", "ReviewedOn", "StartDate", "StartTimeLearning", "StartYear", "Status" },
                values: new object[] { 1L, 18, 18, 1L, new DateTime(2022, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Class code", 1, 1L, new DateTime(2022, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 0, 0, 0), 1L, 1L, 1L, 1L, 1L, 1L, 1L, 1L, 1L, 1L, "Class name", 20, 1L, new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), 2022, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "TrainingPrograms",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
