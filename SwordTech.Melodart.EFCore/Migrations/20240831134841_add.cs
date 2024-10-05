using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwordTech.Melodart.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
