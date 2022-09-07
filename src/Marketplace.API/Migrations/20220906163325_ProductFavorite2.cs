using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class ProductFavorite2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 6, 16, 33, 25, 397, DateTimeKind.Utc).AddTicks(4561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 16, 29, 40, 902, DateTimeKind.Utc).AddTicks(6613));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 6, 16, 29, 40, 902, DateTimeKind.Utc).AddTicks(6613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 16, 33, 25, 397, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");
        }
    }
}
