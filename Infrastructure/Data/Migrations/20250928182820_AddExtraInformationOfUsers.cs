using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddExtraInformationOfUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar columnas nuevas a la tabla Users
            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Users",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Users",
                type: "decimal(5,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar las columnas en caso de rollback
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Users");
        }
    }
}