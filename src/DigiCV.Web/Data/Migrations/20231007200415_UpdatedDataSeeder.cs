using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDataSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "86a7211d-8943-4941-aaf1-2131e66730af", new DateTime(2023, 10, 8, 2, 4, 14, 347, DateTimeKind.Local).AddTicks(8359), "MANAGER", "AQAAAAIAAYagAAAAENrYiTry5SDyxZhqwo1jMxC/kuckh8wkxpWOmFrzIm1PyUAbPw8n1ieYqt+NFDYZZQ==", "manager" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "6173a321-657a-4d1a-ba91-bea469cb0950", new DateTime(2023, 10, 8, 2, 4, 14, 189, DateTimeKind.Local).AddTicks(2106), "ADMIN", "AQAAAAIAAYagAAAAEE5UU5D8THuqIL0qQqPR93VgJn0oFD2WpwPoGx2VG/F4JmwKXNoPGDg48PZKnljFZQ==", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "50eaa539-50b3-457e-bcb3-9dc3f498972e", new DateTime(2023, 10, 3, 23, 13, 22, 920, DateTimeKind.Local).AddTicks(4295), "MANAGER@DIGICV.COM", "AQAAAAIAAYagAAAAENOqQKs8iX+GkCFZAU7BdFvlkJZgtCPoimmiwmU7kI2VHJO/C4Ni0uSLftYzr+rgTQ==", "manager@digicv.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "561a4cb8-5b54-4a11-928e-b699d5ef687b", new DateTime(2023, 10, 3, 23, 13, 22, 769, DateTimeKind.Local).AddTicks(651), "ADMIN@DIGICV.COM", "AQAAAAIAAYagAAAAEE1UPHsvIizecxhRCX6M8jCzUEWewtltk5sDl27iwr8nAtTsVC+SjmWU1FcEt0fAMg==", "admin@digicv.com" });
        }
    }
}
