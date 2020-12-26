using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class EntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ItemImage");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "ItemImage");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "ItemImage");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "ItemImage");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItemStructure",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemStructure");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ItemImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "ItemImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
