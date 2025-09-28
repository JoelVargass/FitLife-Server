using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserHasMultiplePlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Plans_PlanId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlanId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Plans",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UserId",
                table: "Plans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_UserId",
                table: "Plans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_UserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_UserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Plans");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlanId",
                table: "Users",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Plans_PlanId",
                table: "Users",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
