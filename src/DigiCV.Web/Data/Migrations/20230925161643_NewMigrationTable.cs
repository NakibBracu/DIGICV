using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience");

            migrationBuilder.AlterColumn<string>(
                name: "Trainings",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Reference",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CvId",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "228c5a61-e839-4690-afcb-a04b5cbfd3f2", new DateTime(2023, 9, 25, 22, 16, 42, 327, DateTimeKind.Local).AddTicks(9834), "AQAAAAIAAYagAAAAEOta3Ghbot9l6G7dvIH1jgFJ7cCBGQWIxdun6QKhZdsFq9lVixi5c9VmHpUeOLqaXw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "fb3c076b-a9ca-4fd0-a82a-e6d537ad8c1a", new DateTime(2023, 9, 25, 22, 16, 42, 97, DateTimeKind.Local).AddTicks(690), "AQAAAAIAAYagAAAAEE4YacKIgRjBaKTXcH6qP3RKbsTcnlswhLKQigX3NIdPfjFMTGkX+MDQu9FPIZsBRQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience");

            migrationBuilder.AlterColumn<string>(
                name: "Trainings",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Resume",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Reference",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CvId",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "ef23bd84-c692-4030-83b8-7d2f1fe9f6fb", new DateTime(2023, 9, 23, 20, 18, 26, 97, DateTimeKind.Local).AddTicks(8938), "AQAAAAIAAYagAAAAEJ8XgGO7ArVXWwQD/q9SEIL93kinfqHIbeXQdqnPXVetvu0h5ajWsdIbHGGsH7E0PQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "3e6c03b7-bbdc-407c-96f8-03ce604dd5e7", new DateTime(2023, 9, 23, 20, 18, 25, 962, DateTimeKind.Local).AddTicks(2249), "AQAAAAIAAYagAAAAEMtPn0JicGBWy7WZQqo4Eq4eBTUN4t6bOqlKrh1FP43ZIP7cULgo7z8l6FJswENEMQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
