using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class ProductFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 6, 16, 29, 40, 902, DateTimeKind.Utc).AddTicks(6613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 2, 30, 28, 620, DateTimeKind.Utc).AddTicks(1529));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 6, 2, 30, 28, 620, DateTimeKind.Utc).AddTicks(1529),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 6, 16, 29, 40, 902, DateTimeKind.Utc).AddTicks(6613));
        }
    }
}
