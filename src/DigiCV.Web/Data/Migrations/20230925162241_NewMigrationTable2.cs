using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeTemplateId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "37605989-ac8b-4842-bc11-30ab08d2f092", new DateTime(2023, 9, 25, 22, 22, 41, 171, DateTimeKind.Local).AddTicks(125), "AQAAAAIAAYagAAAAEF3IGQh4Qy2Z2RDZEJz6koMrf2rcU1b4FKQXAwnN1wgfiGQQG6bXTu4ZkmE2IUx4BA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "ddf72359-53b9-4e37-afe5-6aa25d1fd7a7", new DateTime(2023, 9, 25, 22, 22, 40, 950, DateTimeKind.Local).AddTicks(2295), "AQAAAAIAAYagAAAAEAtVZGfoxT2nddRg/SwYWjBUMnMOyL/h/Ox9PexUZq+IHGNqGP079l0UC+uDEM59/A==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ResumeTemplateId",
                table: "Resume",
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
                values: new object[] { "228c5a61-e839-4690-afcb-a04b5cbfd3f2", new DateTime(2023, 9, 25, 22, 16, 42, 327, DateTimeKind.Local).AddTicks(9834), "AQAAAAIAAYagAAAAEOta3Ghbot9l6G7dvIH1jgFJ7cCBGQWIxdun6QKhZdsFq9lVixi5c9VmHpUeOLqaXw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "fb3c076b-a9ca-4fd0-a82a-e6d537ad8c1a", new DateTime(2023, 9, 25, 22, 16, 42, 97, DateTimeKind.Local).AddTicks(690), "AQAAAAIAAYagAAAAEE4YacKIgRjBaKTXcH6qP3RKbsTcnlswhLKQigX3NIdPfjFMTGkX+MDQu9FPIZsBRQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
