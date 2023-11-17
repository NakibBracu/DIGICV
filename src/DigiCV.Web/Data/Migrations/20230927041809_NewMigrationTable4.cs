using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Resume",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Reference",
                newName: "PhoneNumber");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "0bd968d0-fd3d-46ab-9e6b-6d122043b1df", new DateTime(2023, 9, 27, 10, 18, 9, 281, DateTimeKind.Local).AddTicks(6019), "AQAAAAIAAYagAAAAED+EIos4sLOF2yjD4S371dcVbWxf4N4lBxEdEKo4euD/RePsnpd8aBE6iHpBNhjL1Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "824a697c-513d-4bd7-9ef0-8e0bd38527a2", new DateTime(2023, 9, 27, 10, 18, 9, 87, DateTimeKind.Local).AddTicks(2803), "AQAAAAIAAYagAAAAENRzrSByx0wYKWPiKSmOszukSTlBoCtdJnR4purNWLKJdy6kTbt90In2zOe+7jv3Rg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Resume",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Reference",
                newName: "Number");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9a66aa32-a7c7-44df-9872-1618c9c62845", new DateTime(2023, 9, 26, 12, 15, 34, 850, DateTimeKind.Local).AddTicks(9886), "AQAAAAIAAYagAAAAEHH8IES7bz1s8WjdMb5TTsEja6jKiHiK/XuxRLbcLTCBmPY/1CXm9XiWP5g5P7y19w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "641d77da-f16f-46ec-a0c3-ac1b071e0539", new DateTime(2023, 9, 26, 12, 15, 34, 780, DateTimeKind.Local).AddTicks(1826), "AQAAAAIAAYagAAAAEIviAWqC/uU5d1wCzCHcl2tYvpkglGFiOPLWKG24zvvtmbOnOwjHJEOLpwkyR6mXQA==" });
        }
    }
}
