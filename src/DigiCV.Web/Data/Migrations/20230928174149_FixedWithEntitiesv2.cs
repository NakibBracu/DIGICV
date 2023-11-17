using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedWithEntitiesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Template",
                newName: "ImageURL");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Template",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9b62870e-4ae8-498d-94b9-af38519aaadf", new DateTime(2023, 9, 27, 13, 9, 9, 841, DateTimeKind.Local).AddTicks(372), "AQAAAAIAAYagAAAAEOAmm9/TtSZHEQ/F+TbCzOQPtAskKno9duLqMJ75kd0rU+cKR4zOJAHePVeaV58MyQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "20fcdf3a-5000-4f54-a761-d468e6471ad1", new DateTime(2023, 9, 27, 13, 9, 9, 792, DateTimeKind.Local).AddTicks(4995), "AQAAAAIAAYagAAAAENJbXgsgfZXZoozZxYbMkZScZQ2fa4AIcWCSb4/QcoWVMIMnV8KK4eFU9trmU8wIog==" });
        }
    }
}
