using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedContactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "d7d2229c-45c4-4ccc-b292-bbc333b20797", new DateTime(2023, 10, 12, 1, 28, 0, 851, DateTimeKind.Local).AddTicks(7586), "AQAAAAIAAYagAAAAEI24PSRCbyXRpiJfhh9oDHtvNcXGLn86pqMwiwJJ0YwG+fM5rXhFealtEBU6ZqN7JQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "8656f5ca-bf2b-4c6e-85b3-0b59669902ed", new DateTime(2023, 10, 12, 1, 28, 0, 803, DateTimeKind.Local).AddTicks(3956), "AQAAAAIAAYagAAAAEI5RxX7zhazDOdnMeg8KifnShLP+NxfcgNLtd+1GMiI3ug8ufu2+IYqDc7MmATyeyQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

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
        }
    }
}
