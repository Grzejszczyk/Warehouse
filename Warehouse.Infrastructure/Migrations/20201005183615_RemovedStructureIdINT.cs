using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class RemovedStructureIdINT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Structures_StructureId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "StructureId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Structures_StructureId",
                table: "Items",
                column: "StructureId",
                principalTable: "Structures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Structures_StructureId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "StructureId",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Structures_StructureId",
                table: "Items",
                column: "StructureId",
                principalTable: "Structures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
