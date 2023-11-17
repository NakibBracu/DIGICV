using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiCV.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResumeTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Cv_CvId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Cv_CvId",
                table: "Experience");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Cv_CvId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Reference_Cv_CvId",
                table: "Reference");

            migrationBuilder.DropTable(
                name: "SkillCv");

            migrationBuilder.DropTable(
                name: "Cv");

            migrationBuilder.CreateTable(
                name: "ResumeTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResumeTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trainings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resume_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                        column: x => x.ResumeTemplateId,
                        principalTable: "ResumeTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    CvId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkill", x => new { x.CvId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ResumeSkill_Resume_CvId",
                        column: x => x.CvId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ApplicationUserId",
                table: "Resume",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkill_SkillId",
                table: "ResumeSkill",
                column: "SkillId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "ResumeSkill");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "ResumeTemplate");

            migrationBuilder.CreateTable(
                name: "Cv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trainings = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cv_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillCv",
                columns: table => new
                {
                    CvId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCv", x => new { x.CvId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_SkillCv_Cv_CvId",
                        column: x => x.CvId,
                        principalTable: "Cv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillCv_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Cv_ApplicationUserId",
                table: "Cv",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillCv_SkillId",
                table: "SkillCv",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Cv_CvId",
                table: "Education",
                column: "CvId",
                principalTable: "Cv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Cv_CvId",
                table: "Experience",
                column: "CvId",
                principalTable: "Cv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Cv_CvId",
                table: "Project",
                column: "CvId",
                principalTable: "Cv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reference_Cv_CvId",
                table: "Reference",
                column: "CvId",
                principalTable: "Cv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
