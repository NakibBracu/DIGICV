using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_CvId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Resume_CvId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Reference_Resume_CvId",
                table: "Reference");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkill_Resume_CvId",
                table: "ResumeSkill");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "ResumeSkill",
                newName: "ResumeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Reference",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reference_CvId",
                table: "Reference",
                newName: "IX_Reference_ResumeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Project",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CvId",
                table: "Project",
                newName: "IX_Project_ResumeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Experience",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Experience_CvId",
                table: "Experience",
                newName: "IX_Experience_ResumeId");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Education",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_CvId",
                table: "Education",
                newName: "IX_Education_ResumeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Resume_ResumeId",
                table: "Project",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reference_Resume_ResumeId",
                table: "Reference",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkill_Resume_ResumeId",
                table: "ResumeSkill",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Resume_ResumeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Resume_ResumeId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Resume_ResumeId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Reference_Resume_ResumeId",
                table: "Reference");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeSkill_Resume_ResumeId",
                table: "ResumeSkill");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "ResumeSkill",
                newName: "CvId");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Reference",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_Reference_ResumeId",
                table: "Reference",
                newName: "IX_Reference_CvId");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Project",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ResumeId",
                table: "Project",
                newName: "IX_Project_CvId");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Experience",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_Experience_ResumeId",
                table: "Experience",
                newName: "IX_Experience_CvId");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Education",
                newName: "CvId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ResumeId",
                table: "Education",
                newName: "IX_Education_CvId");

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
                name: "FK_Education_Resume_CvId",
                table: "Education",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Resume_CvId",
                table: "Experience",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Resume_CvId",
                table: "Project",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reference_Resume_CvId",
                table: "Reference",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeSkill_Resume_CvId",
                table: "ResumeSkill",
                column: "CvId",
                principalTable: "Resume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
