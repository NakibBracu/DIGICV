using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResumeSeederAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "b3d72367-8535-4415-95ee-375ee4798578", new DateTime(2023, 10, 27, 14, 54, 48, 695, DateTimeKind.Local).AddTicks(3264), "AQAAAAIAAYagAAAAECo00R71nxtX2wWzx/Ku4c1oGd0zXGx1vznkaDtORY1krJPJegDXggjsTulSu+pNSQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9923135c-e82e-42de-80f1-2142abf31743", new DateTime(2023, 10, 27, 14, 54, 48, 492, DateTimeKind.Local).AddTicks(1817), "AQAAAAIAAYagAAAAEIgzuKoKOEs6Xk4czrJfBEbNKftpzu+UACG22sG/pAU74WIsKnH7c4FZQji6pg/Yuw==" });

            migrationBuilder.InsertData(
                table: "ResumeTemplate",
                columns: new[] { "Id", "ImageName", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("933b1bdb-19df-4e24-bf1f-49f5abd8aef6"), "34a90fa4-ce8a-499c-8182-852a7125141f.png", true, "Modern" },
                    { new Guid("933b1bdb-19df-4e24-bf1f-49f5abd8aef7"), "22ce39f1-a576-4e66-a9b3-312552c95dde.png", true, "Standard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResumeTemplate",
                keyColumn: "Id",
                keyValue: new Guid("933b1bdb-19df-4e24-bf1f-49f5abd8aef6"));

            migrationBuilder.DeleteData(
                table: "ResumeTemplate",
                keyColumn: "Id",
                keyValue: new Guid("933b1bdb-19df-4e24-bf1f-49f5abd8aef7"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "86a7211d-8943-4941-aaf1-2131e66730af", new DateTime(2023, 10, 8, 2, 4, 14, 347, DateTimeKind.Local).AddTicks(8359), "AQAAAAIAAYagAAAAENrYiTry5SDyxZhqwo1jMxC/kuckh8wkxpWOmFrzIm1PyUAbPw8n1ieYqt+NFDYZZQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "6173a321-657a-4d1a-ba91-bea469cb0950", new DateTime(2023, 10, 8, 2, 4, 14, 189, DateTimeKind.Local).AddTicks(2106), "AQAAAAIAAYagAAAAEE5UU5D8THuqIL0qQqPR93VgJn0oFD2WpwPoGx2VG/F4JmwKXNoPGDg48PZKnljFZQ==" });
        }
    }
}
