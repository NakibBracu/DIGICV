using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class CoverLetterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverLetters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddressEx = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientAddressing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverLetters", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "01c98e6d-cf95-4449-b33b-3f4a9894b263", new DateTime(2023, 10, 23, 22, 29, 8, 69, DateTimeKind.Local).AddTicks(2202), "AQAAAAIAAYagAAAAEF9b3T4L1wisgyBvxlkXkZiyq7GxLg+qc9EuZcdb1DaHxZR8xQm6fhWy/k3g5Gkcgg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "114d0ca7-bd8b-4b8a-96c7-0d7fd494d95a", new DateTime(2023, 10, 23, 22, 29, 7, 975, DateTimeKind.Local).AddTicks(219), "AQAAAAIAAYagAAAAEMkP+l6m2Tu64Yxgz7GYBOEzpJePgjO/88hS1l7oMvgG+MyG+bM5oP5lKjx1ObJjPQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverLetters");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "37767162-fd2c-4412-8611-17f702049215", new DateTime(2023, 10, 18, 23, 33, 47, 872, DateTimeKind.Local).AddTicks(3455), "AQAAAAIAAYagAAAAEEvTAMwTMmIy8FzCf7wv4a63LxGpzaLwcIrR35XOHZJh/9URDqIapxqF40iR1SjW4g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "16638dce-57cc-4704-bf85-22d982f8fef7", new DateTime(2023, 10, 18, 23, 33, 47, 766, DateTimeKind.Local).AddTicks(1978), "AQAAAAIAAYagAAAAEOQa08lomznjuuYos4+yzTYj7tJBuA9OdLdGD9jt+G8JrUA5Yx5fvjV45jLDf7BQmw==" });
        }
    }
}
