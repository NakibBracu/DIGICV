using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixedTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "d930ea61-751a-432d-be00-c61e22260069", new DateTime(2023, 9, 29, 13, 56, 8, 127, DateTimeKind.Local).AddTicks(7373), "AQAAAAIAAYagAAAAEA2UIKNNK3yw06G33tyzT1an2AkCq+1ekUzqqCjnwGNoCJuzYmsAth/JRd/Q/Euvxw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "58c4a01a-d88d-4664-976a-3ce537991fec", new DateTime(2023, 9, 29, 13, 56, 8, 45, DateTimeKind.Local).AddTicks(6224), "AQAAAAIAAYagAAAAEIydSlP7tbZjB2iviPdKXs3i/QHTnvlqcVc92T105Ms/Dm8A0lGTlCT4dsR1rnd+0Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
