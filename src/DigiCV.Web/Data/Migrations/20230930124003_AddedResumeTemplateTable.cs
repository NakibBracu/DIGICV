using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedResumeTemplateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ResumeTemplate",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ResumeTemplate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "e2bf8e97-2d88-4889-ab07-a545b020ec2d", new DateTime(2023, 9, 28, 23, 41, 48, 719, DateTimeKind.Local).AddTicks(7724), "AQAAAAIAAYagAAAAEF4T5JAbb67Ztu31V1QMOmjaWdclZGXC0+9HTE466Jv64GAFe52AnBILfQwTQb3q5A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "536771dc-c797-4dc5-b1fa-a2d3d936e980", new DateTime(2023, 9, 28, 23, 41, 48, 648, DateTimeKind.Local).AddTicks(1225), "AQAAAAIAAYagAAAAEPvrsgmLuT3YiJ63jYM6/TAJ5+K7ttYFbUYMVG0KTOBfFKQuowZEzDFmzNuWnxMwRg==" });
        }
    }
}
