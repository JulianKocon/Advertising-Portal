using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertisingPortal.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "IdAdvertisement", "Date", "Description", "IdRegion", "IdUser", "Name", "Price", "Region", "RegionIdRegion" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Old but gold", 1, 1, "Toyota mr2", 24000m, null, null },
                    { 2, new DateTime(2022, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "White Maine Coon for sale", 1, 1, "Pets", 1000m, null, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "Name" },
                values: new object[,]
                {
                    { 1, "Vehicles" },
                    { 2, "Real estate" },
                    { 3, "House and garden" },
                    { 4, "Pets" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "IdRegion", "Name" },
                values: new object[,]
                {
                    { 1, "Warsaw, Poland" },
                    { 2, "Gdynia, Poland" },
                    { 3, "New York, USA" },
                    { 4, "Dallas, USA" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "IdAdvertisement",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Advertisements",
                keyColumn: "IdAdvertisement",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "IdRegion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "IdRegion",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "IdRegion",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "IdRegion",
                keyValue: 4);
        }
    }
}
