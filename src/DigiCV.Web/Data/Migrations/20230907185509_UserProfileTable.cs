using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoiningDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GithubUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash", "PhoneNumber", "UserProfileId" },
                values: new object[] { "e3ef89fc-1eb6-4ac3-ad01-84a9a3906869", new DateTime(2023, 9, 8, 0, 55, 8, 975, DateTimeKind.Local).AddTicks(5210), "AQAAAAIAAYagAAAAEA7Soocyxhfg8uTTt/PMaWJBLLFqi9LWooqLYhxs8So7DBdnma497WHUykAEHG5jZw==", "+8801856817465", new Guid("a618e70b-3cdb-420a-a1b3-702d0b06e9c2") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash", "PhoneNumber", "UserProfileId" },
                values: new object[] { "b7690625-e629-4897-b9fe-5545fd41d135", new DateTime(2023, 9, 8, 0, 55, 8, 833, DateTimeKind.Local).AddTicks(212), "AQAAAAIAAYagAAAAEHw5k7iny/cB4Myeo+1ck3K0rVAWuDMw+ix7wh5b/Sxj+IyXHTt74CC7xxsUxlOl0g==", "+8801856817465", new Guid("9749e4e1-f89c-42bd-8a04-5a79993a58f9") });

            migrationBuilder.InsertData(
                table: "UserProfile",
                columns: new[] { "Id", "Address", "Designation", "Education", "Experience", "GithubUsername", "ImageUrl", "IsActive", "LinkedInUsername" },
                values: new object[,]
                {
                    { new Guid("9749e4e1-f89c-42bd-8a04-5a79993a58f9"), "Room No. 419, BSMRH Hall, MBSTU", ".NET Developer", "Mawlana Bhashani Science and Technology University, Tangail", "C#, ASP.NET, EF Core", "absa1am", null, true, "absa1am" },
                    { new Guid("a618e70b-3cdb-420a-a1b3-702d0b06e9c2"), "Room No. 419, BSMRH Hall, MBSTU", ".NET Developer", "Mawlana Bhashani Science and Technology University, Tangail", "C#, ASP.NET, EF Core", "absa1am", null, true, "absa1am" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileId",
                table: "AspNetUsers",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserProfile_UserProfileId",
                table: "AspNetUsers",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserProfile_UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JoiningDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "fae2bcf6-cbc7-4ee8-9859-f22d13d5b819", "AQAAAAIAAYagAAAAEOxwkU5Ri8PHpcfghiznJPPYcaQI2ez8CnKCXA81y9IFo96KZdOS81y5PsqAUMKTNQ==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber" },
                values: new object[] { "a498a7b3-21bf-40bf-bc65-e05d6f065397", "AQAAAAIAAYagAAAAEK3XuCvoaGBhdiafo9y9v3BgLz4E/FThr06/0N2PfFqc4zugWEQgdJjXq0j3hhkP7w==", null });
        }
    }
}
