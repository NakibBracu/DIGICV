using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "20e5fb16-6ee5-4625-813f-c8a783fda185", new DateTime(2023, 10, 4, 1, 3, 10, 633, DateTimeKind.Local).AddTicks(5301), "AQAAAAIAAYagAAAAEDiZgn6y/nHsudX0cvbxDZrsDOjgZm3lA2J098OVNhYmtTY8+S5eyi7vsPDpV0N5tQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "7dc50426-639f-492d-b579-38b3a8786fe6", new DateTime(2023, 10, 4, 1, 3, 10, 561, DateTimeKind.Local).AddTicks(7847), "AQAAAAIAAYagAAAAEB20grptU/I4oN3D+m3vl1pAeSoxoiAuxF7j01KpGqXcCn4EBUs7FgrF3/hdtFkc3w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9d234fdb-80e8-4afe-8022-b1711106924f", new DateTime(2023, 9, 16, 13, 28, 22, 814, DateTimeKind.Local).AddTicks(6073), "AQAAAAIAAYagAAAAEPQzVvp+Zr4K+9Rj4Rh+btGXvfNya2kdx9OwZwuETPY7M0hT0x0xnW0ML6p1WxFkRQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "e1dab3c8-beae-46aa-84d7-2d5c72655130", new DateTime(2023, 9, 16, 13, 28, 22, 664, DateTimeKind.Local).AddTicks(576), "AQAAAAIAAYagAAAAEPmZ6Z78MlbrnhHlw3nYLSKWVtwMV+ERhKnvYygDRSWQJs/YqfSxfSeaYoY1XUSH9g==" });
        }
    }
}
