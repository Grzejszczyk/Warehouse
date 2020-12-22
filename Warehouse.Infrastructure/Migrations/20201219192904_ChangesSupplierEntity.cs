using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class ChangesSupplierEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonEmail",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonName",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonPhoneNo",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonSurname",
                table: "Suppliers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPersonEmail",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactPersonName",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactPersonPhoneNo",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactPersonSurname",
                table: "Suppliers");

            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_SupplierId",
                table: "ContactPerson",
                column: "SupplierId",
                unique: true);
        }
    }
}
