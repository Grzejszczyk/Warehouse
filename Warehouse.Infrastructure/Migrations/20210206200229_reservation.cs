using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckOutFromShopStore",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "IsCheckInFromShopStore",
                table: "CheckIns");

            migrationBuilder.AddColumn<bool>(
                name: "OrderNo",
                table: "CheckOuts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrderNo",
                table: "CheckIns",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "CheckIns");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckOutFromShopStore",
                table: "CheckOuts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckInFromShopStore",
                table: "CheckIns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
