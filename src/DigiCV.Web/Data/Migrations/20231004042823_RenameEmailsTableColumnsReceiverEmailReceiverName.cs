using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmailsTableColumnsReceiverEmailReceiverName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Emails",
                newName: "ReceiverName");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Emails",
                newName: "ReceiverEmail");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "3afddefe-752b-4f66-aaf3-eedda37c9c25", new DateTime(2023, 10, 4, 10, 28, 23, 151, DateTimeKind.Local).AddTicks(2946), "AQAAAAIAAYagAAAAEBw5tCP7kG7z6rRaNFOLbBOz4Z7wqoVTUUpU5GSSUIc0+oD7DN5Z04A9UZtKxbv9Hw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "4c757a25-311a-4e0b-a790-b04420dc53cd", new DateTime(2023, 10, 4, 10, 28, 22, 934, DateTimeKind.Local).AddTicks(1168), "AQAAAAIAAYagAAAAEJIypqJfLBbwA05j2JZGqCg7PUc2W7liuIPLwvrRNanPtKkOKT4ypd452EfJZgaywQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "Emails",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "ReceiverEmail",
                table: "Emails",
                newName: "From");

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
    }
}
