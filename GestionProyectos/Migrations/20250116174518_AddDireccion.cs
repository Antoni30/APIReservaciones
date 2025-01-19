using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionProyectos.Migrations
{
    /// <inheritdoc />
    public partial class AddDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "direcccion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "direcccion",
                table: "Clientes");
        }
    }
}
