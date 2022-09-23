using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class UserForeignKeyIgnoredV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 23, 9, 51, 39, 187, DateTimeKind.Utc).AddTicks(936),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 23, 9, 50, 7, 902, DateTimeKind.Utc).AddTicks(3230));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 23, 9, 50, 7, 902, DateTimeKind.Utc).AddTicks(3230),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 23, 9, 51, 39, 187, DateTimeKind.Utc).AddTicks(936));

            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
