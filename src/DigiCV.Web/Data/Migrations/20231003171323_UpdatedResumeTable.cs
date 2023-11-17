using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedResumeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "50eaa539-50b3-457e-bcb3-9dc3f498972e", new DateTime(2023, 10, 3, 23, 13, 22, 920, DateTimeKind.Local).AddTicks(4295), "AQAAAAIAAYagAAAAENOqQKs8iX+GkCFZAU7BdFvlkJZgtCPoimmiwmU7kI2VHJO/C4Ni0uSLftYzr+rgTQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "561a4cb8-5b54-4a11-928e-b699d5ef687b", new DateTime(2023, 10, 3, 23, 13, 22, 769, DateTimeKind.Local).AddTicks(651), "AQAAAAIAAYagAAAAEE1UPHsvIizecxhRCX6M8jCzUEWewtltk5sDl27iwr8nAtTsVC+SjmWU1FcEt0fAMg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Resume");

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
