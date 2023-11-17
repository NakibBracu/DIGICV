using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsersDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"), 0, "fae2bcf6-cbc7-4ee8-9859-f22d13d5b819", "manager@digicv.com", true, "Manager", true, null, "MANAGER@DIGICV.COM", "MANAGER@DIGICV.COM", "AQAAAAIAAYagAAAAEOxwkU5Ri8PHpcfghiznJPPYcaQI2ez8CnKCXA81y9IFo96KZdOS81y5PsqAUMKTNQ==", null, false, "FC37C84E276C4D978DF9054129D0CB23", false, "manager@digicv.com" },
                    { new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"), 0, "a498a7b3-21bf-40bf-bc65-e05d6f065397", "admin@digicv.com", true, "Admin", true, null, "ADMIN@DIGICV.COM", "ADMIN@DIGICV.COM", "AQAAAAIAAYagAAAAEK3XuCvoaGBhdiafo9y9v3BgLz4E/FThr06/0N2PfFqc4zugWEQgdJjXq0j3hhkP7w==", null, false, "BFCC7B453A8B4B6C8A4C93EE28A3B4A8", false, "admin@digicv.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Administrator", "Administrator", new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48") },
                    { 2, "Manager", "Manager", new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"));
        }
    }
}
