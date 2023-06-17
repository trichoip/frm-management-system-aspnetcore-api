using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class seedhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClassUpdateHistories",
                columns: new[] { "IdClass", "ModifyBy", "UpdateDate" },
                values: new object[] { 1L, 1L, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "HistoryTrainingPrograms",
                columns: new[] { "IdProgram", "IdUser", "ModifiedOn" },
                values: new object[] { 1L, 1L, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClassUpdateHistories",
                keyColumns: new[] { "IdClass", "ModifyBy", "UpdateDate" },
                keyValues: new object[] { 1L, 1L, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "HistoryTrainingPrograms",
                keyColumns: new[] { "IdProgram", "IdUser" },
                keyValues: new object[] { 1L, 1L });
        }
    }
}
