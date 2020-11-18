using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class StructureModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subassembly",
                table: "Structures");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Structures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Structures");

            migrationBuilder.AddColumn<string>(
                name: "Subassembly",
                table: "Structures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
