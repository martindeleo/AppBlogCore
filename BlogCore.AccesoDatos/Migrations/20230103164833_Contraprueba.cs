using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppBlogCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Contraprueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prueba",
                table: "Slider");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "prueba",
                table: "Slider",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
