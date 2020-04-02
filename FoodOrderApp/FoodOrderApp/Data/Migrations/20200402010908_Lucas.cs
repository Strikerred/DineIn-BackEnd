using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrderApp.Data.Migrations
{
    public partial class Lucas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuSection",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleVM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleVM");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuSection",
                table: "MenuItems");
        }
    }
}
