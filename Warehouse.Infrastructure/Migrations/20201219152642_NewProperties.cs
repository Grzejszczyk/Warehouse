using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class NewProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatalogNumber",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrawingNumber",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MagazineZoneId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckOutFromShopStore",
                table: "CheckOuts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckInFromShopStore",
                table: "CheckIns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPerson_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemImage_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MagazineZones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazineZones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_MagazineZoneId",
                table: "Items",
                column: "MagazineZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_SupplierId",
                table: "ContactPerson",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemImage_ItemId",
                table: "ItemImage",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MagazineZones_MagazineZoneId",
                table: "Items",
                column: "MagazineZoneId",
                principalTable: "MagazineZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MagazineZones_MagazineZoneId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.DropTable(
                name: "ItemImage");

            migrationBuilder.DropTable(
                name: "MagazineZones");

            migrationBuilder.DropIndex(
                name: "IX_Items_MagazineZoneId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CatalogNumber",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DrawingNumber",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MagazineZoneId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsCheckOutFromShopStore",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "IsCheckInFromShopStore",
                table: "CheckIns");
        }
    }
}
