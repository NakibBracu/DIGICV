using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserKeyFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_AspNetUsers_ApplicationUserId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ApplicationUserId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5f4c76d3-79b0-4923-86a7-511ac60c2ab9"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "702160ec-2a42-4c5c-abbb-54e235c3c3c8", new DateTime(2023, 10, 26, 15, 51, 40, 122, DateTimeKind.Local).AddTicks(2381), "AQAAAAIAAYagAAAAEPRgOzq5asaPJ6I1qAxEZvqs3alqqn9kC8KJ8YLSeBnP67zVv/RHb1akwlanW3xSKg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e26967f0-ce4c-4c14-8a0b-45beb8c9eb48"),
                columns: new[] { "ConcurrencyStamp", "JoiningDate", "PasswordHash" },
                values: new object[] { "1ae04ed2-ed88-44cb-bfae-963611e8d77e", new DateTime(2023, 10, 26, 15, 51, 39, 946, DateTimeKind.Local).AddTicks(7201), "AQAAAAIAAYagAAAAEB/lFc2dXgGUxp2GillqO0FAHoINtW8P5XERwHc6B1s5i0CsUfRdOyWfC0ZY5N+u7w==" });

            migrationBuilder.CreateIndex(
                name: "IX_Resume_UserId",
                table: "Resume",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoverLetters_UserId",
                table: "CoverLetters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverLetters_AspNetUsers_UserId",
                table: "CoverLetters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_AspNetUsers_UserId",
                table: "Resume",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverLetters_AspNetUsers_UserId",
                table: "CoverLetters");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_AspNetUsers_UserId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_UserId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_CoverLetters_UserId",
                table: "CoverLetters");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ApplicationUserId",
                table: "Resume",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_AspNetUsers_ApplicationUserId",
                table: "Resume",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
