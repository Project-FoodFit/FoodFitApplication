using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFit.Migrations
{
    public partial class migrateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Recipe");
        }
    }
}
