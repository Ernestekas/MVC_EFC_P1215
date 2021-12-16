using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApp.Migrations
{
    public partial class dataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShopItems",
                columns: new[] { "Id", "ExpiryDate", "Name", "ShopId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 15, 21, 8, 56, 325, DateTimeKind.Utc).AddTicks(2390), "Tiristorius", null },
                    { 2, new DateTime(2021, 12, 15, 21, 8, 56, 325, DateTimeKind.Utc).AddTicks(3092), "Transformatorius", null },
                    { 3, new DateTime(2021, 12, 15, 21, 8, 56, 325, DateTimeKind.Utc).AddTicks(3096), "Mėlynos kojinės", null },
                    { 4, new DateTime(2021, 12, 15, 21, 8, 56, 325, DateTimeKind.Utc).AddTicks(3098), "Raudonos kojinės-pirštinės", null },
                    { 5, new DateTime(2021, 12, 15, 21, 8, 56, 325, DateTimeKind.Utc).AddTicks(3099), "Permatomos kojinės", null }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Robotukai" },
                    { 2, "Kojinės ir aš" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShopItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
