using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisingPortal.Migrations
{
    public partial class AddedIsAvailableToAdvertisementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Regions_RegionIdRegion",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_RegionIdRegion",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "RegionIdRegion",
                table: "Advertisements");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_IdRegion",
                table: "Advertisements",
                column: "IdRegion");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Regions_IdRegion",
                table: "Advertisements",
                column: "IdRegion",
                principalTable: "Regions",
                principalColumn: "IdRegion",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Regions_IdRegion",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_IdRegion",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionIdRegion",
                table: "Advertisements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_RegionIdRegion",
                table: "Advertisements",
                column: "RegionIdRegion");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Regions_RegionIdRegion",
                table: "Advertisements",
                column: "RegionIdRegion",
                principalTable: "Regions",
                principalColumn: "IdRegion");
        }
    }
}
