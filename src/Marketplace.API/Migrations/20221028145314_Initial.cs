using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 28, 14, 53, 13, 768, DateTimeKind.Utc).AddTicks(6732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 28, 14, 51, 2, 525, DateTimeKind.Utc).AddTicks(118));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 28, 14, 51, 2, 525, DateTimeKind.Utc).AddTicks(118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 28, 14, 53, 13, 768, DateTimeKind.Utc).AddTicks(6732));
        }
    }
}
