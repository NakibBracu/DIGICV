using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "c39ac3d7-09cb-486f-8544-fc4dd6ff83c9", new DateTime(2023, 10, 28, 3, 44, 46, 949, DateTimeKind.Local).AddTicks(3801), "AQAAAAIAAYagAAAAEJU72hWURWHMCL7m5ZMd9owy+HqRgZdHDg53AExekMqu+r7H5vvYW6mPHxXctSYF7A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "09960ff2-3d4b-41cf-9c4e-57d96c988693", new DateTime(2023, 10, 28, 3, 44, 46, 900, DateTimeKind.Local).AddTicks(2186), "AQAAAAIAAYagAAAAECxZd1huf+2OLCZK18ayYdmtOD/at7yjlVp6v/A+Q/83ty3XIaSd73qU59tn1gWe1w==" });

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("9749e4e1-f89c-42bd-8a04-5a79993a58f9"),
                column: "ImageUrl",
                value: "24fc78b6-8f28-468f-9476-e21e4cfaca6b.png");

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("a618e70b-3cdb-420a-a1b3-702d0b06e9c2"),
                column: "ImageUrl",
                value: "6d0bcabd-744c-4bdd-8e69-8f30088c984d.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "94c411a5-e0cb-44a4-8970-ba0835eddc67", new DateTime(2023, 10, 27, 22, 55, 32, 43, DateTimeKind.Local).AddTicks(4743), "AQAAAAIAAYagAAAAEGv6MbVISTrMIl1SpgAixmNBUPR2wwgpeTetMMGOvZ4SSrmE+sdG3EmnvQTuYE7weQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "7cdc9d50-dddf-4a5f-a010-be2deada33fe", new DateTime(2023, 10, 27, 22, 55, 31, 934, DateTimeKind.Local).AddTicks(2387), "AQAAAAIAAYagAAAAEKptxfdFXcmzG+bvTwrcCL5fKBEzw5ylsReZ7TIv0sWB4uV540cEuz3xy4A5Iz0lvg==" });

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("9749e4e1-f89c-42bd-8a04-5a79993a58f9"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("a618e70b-3cdb-420a-a1b3-702d0b06e9c2"),
                column: "ImageUrl",
                value: null);
        }
    }
}
