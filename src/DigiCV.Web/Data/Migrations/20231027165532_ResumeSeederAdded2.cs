using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResumeSeederAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "b3d72367-8535-4415-95ee-375ee4798578", new DateTime(2023, 10, 27, 14, 54, 48, 695, DateTimeKind.Local).AddTicks(3264), "AQAAAAIAAYagAAAAECo00R71nxtX2wWzx/Ku4c1oGd0zXGx1vznkaDtORY1krJPJegDXggjsTulSu+pNSQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "9923135c-e82e-42de-80f1-2142abf31743", new DateTime(2023, 10, 27, 14, 54, 48, 492, DateTimeKind.Local).AddTicks(1817), "AQAAAAIAAYagAAAAEIgzuKoKOEs6Xk4czrJfBEbNKftpzu+UACG22sG/pAU74WIsKnH7c4FZQji6pg/Yuw==" });
        }
    }
}
