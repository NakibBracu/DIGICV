using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTemplateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");

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
    }
}
