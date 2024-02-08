using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityTrivia.WebApi.Migrations
{
    public partial class AddIsExistentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExistent",
                table: "Cities",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExistent",
                table: "Cities");
        }
    }
}
