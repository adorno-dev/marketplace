using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class Favorite2 : Migration
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
                defaultValue: new DateTime(2022, 9, 6, 22, 31, 51, 319, DateTimeKind.Utc).AddTicks(3308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 16, 33, 25, 397, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");
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
                defaultValue: new DateTime(2022, 9, 6, 16, 33, 25, 397, DateTimeKind.Utc).AddTicks(4561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 22, 31, 51, 319, DateTimeKind.Utc).AddTicks(3308));

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId",
                unique: true);
        }
    }
}
