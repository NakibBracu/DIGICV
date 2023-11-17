using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationTable5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "73e45261-bba0-4d14-98e4-32fcd29a75ec", new DateTime(2023, 10, 2, 18, 27, 27, 622, DateTimeKind.Local).AddTicks(4600), "AQAAAAIAAYagAAAAEBcAGascpPeiyvghjja5eP6CS9jXPhsHgI9JXtp/jdAbuF2zPtOLcV7uQBjOVWXhDw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "56408e02-78ea-40a9-816b-3bec0afb8b28", new DateTime(2023, 10, 2, 18, 27, 27, 480, DateTimeKind.Local).AddTicks(4577), "AQAAAAIAAYagAAAAEGxa02W8yfoaXvw8CvkqAuupnfh3GV3SiYLeLCh4Rzv4nmNo5hbs3cYQ+0j4o1GUFQ==" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, ".Net Core, Web API, NodeJs" },
                    { 2, "Asp.net, .Net MVC" },
                    { 3, "C#, VB.Net" },
                    { 4, "LINQ, SQL, Entity Framework and MongoDB" },
                    { 5, "Angular 6, Angular 1 and MEAN Stack" },
                    { 6, "Javascript, JQuery and Ajax" },
                    { 7, "Bootstrap CSS, HTML" },
                    { 8, "Crystal and RDLC Report" },
                    { 9, "Design Pattern & Principles, OOP Software Design& Architecture" },
                    { 10, "TFS, GIT, V. SourceSafe, Trello, Agile, SCRUM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "c760572c-2677-43e4-b137-ee18ed26ca19", new DateTime(2023, 9, 30, 18, 40, 3, 301, DateTimeKind.Local).AddTicks(4018), "AQAAAAIAAYagAAAAEGjR3Mr4LNQbDSrSYW8LeUOPpzXVcpnYx3Tzib3tKCyTE/okrit7OGdOg9aoEGv6NQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "c251e3ec-114e-4955-a647-0b490fc144a1", new DateTime(2023, 9, 30, 18, 40, 3, 252, DateTimeKind.Local).AddTicks(8051), "AQAAAAIAAYagAAAAEN5hFyUNdALBw4NSEm4dr+ruuyZcpHR1evrO+UiaIrWJMYN4DNUxlgLZcdUy4LvMUQ==" });
        }
    }
}
