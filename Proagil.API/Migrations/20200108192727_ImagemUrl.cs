using Microsoft.EntityFrameworkCore.Migrations;

namespace Proagil.API.Migrations
{
    public partial class ImagemUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImagemUrl",
                table: "Eventos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Eventos");
        }
    }
}
