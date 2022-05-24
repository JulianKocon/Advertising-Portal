using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertisingPortal.Migrations
{
    public partial class BaseAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Users",
                newName: "Region");

            migrationBuilder.AlterColumn<decimal>(
                name: "Money",
                table: "Users",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "IdRegion",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionIdRegion",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    IdRegion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.IdRegion);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "money", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRegion = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionIdRegion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.IdAdvertisement);
                    table.ForeignKey(
                        name: "FK_Advertisements_Regions_RegionIdRegion",
                        column: x => x.RegionIdRegion,
                        principalTable: "Regions",
                        principalColumn: "IdRegion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesIdRole = table.Column<int>(type: "int", nullable: false),
                    UsersIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesIdRole, x.UsersIdUser });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesIdRole",
                        column: x => x.RolesIdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersIdUser",
                        column: x => x.UsersIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementCategory",
                columns: table => new
                {
                    AdvertisementsIdAdvertisement = table.Column<int>(type: "int", nullable: false),
                    CategoriesIdCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementCategory", x => new { x.AdvertisementsIdAdvertisement, x.CategoriesIdCategory });
                    table.ForeignKey(
                        name: "FK_AdvertisementCategory_Advertisements_AdvertisementsIdAdvertisement",
                        column: x => x.AdvertisementsIdAdvertisement,
                        principalTable: "Advertisements",
                        principalColumn: "IdAdvertisement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementCategory_Categories_CategoriesIdCategory",
                        column: x => x.CategoriesIdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdAdvertisement = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => new { x.IdUser, x.IdAdvertisement });
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Advertisements_IdAdvertisement",
                        column: x => x.IdAdvertisement,
                        principalTable: "Advertisements",
                        principalColumn: "IdAdvertisement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegionIdRegion",
                table: "Users",
                column: "RegionIdRegion");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementCategory_CategoriesIdCategory",
                table: "AdvertisementCategory",
                column: "CategoriesIdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_IdUser",
                table: "Advertisements",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_RegionIdRegion",
                table: "Advertisements",
                column: "RegionIdRegion");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_IdAdvertisement",
                table: "PurchaseOrders",
                column: "IdAdvertisement");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersIdUser",
                table: "RoleUser",
                column: "UsersIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Regions_RegionIdRegion",
                table: "Users",
                column: "RegionIdRegion",
                principalTable: "Regions",
                principalColumn: "IdRegion",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Regions_RegionIdRegion",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AdvertisementCategory");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Users_RegionIdRegion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdRegion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegionIdRegion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Users",
                newName: "Roles");

            migrationBuilder.AlterColumn<decimal>(
                name: "Money",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
