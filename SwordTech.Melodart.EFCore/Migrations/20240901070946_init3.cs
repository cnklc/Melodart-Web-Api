using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwordTech.Melodart.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Teacher_TeacherId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_TeacherId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "TeacherDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Departments_DepartmentId1",
                        column: x => x.DepartmentId1,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherDepartments_Teacher_TeacherId1",
                        column: x => x.TeacherId1,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDepartments_DepartmentId",
                table: "TeacherDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDepartments_DepartmentId1",
                table: "TeacherDepartments",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDepartments_TeacherId",
                table: "TeacherDepartments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDepartments_TeacherId1",
                table: "TeacherDepartments",
                column: "TeacherId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherDepartments");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_TeacherId",
                table: "Departments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Teacher_TeacherId",
                table: "Departments",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id");
        }
    }
}
