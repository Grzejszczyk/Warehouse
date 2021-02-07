using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class reservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservedQuantity",
                table: "Items",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                table: "Items");
        }
    }
}
