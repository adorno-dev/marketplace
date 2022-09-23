using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.API.Migrations
{
    public partial class StoreUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "Politics",
                table: "Stores",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "Stores",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Stores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 22, 17, 24, 38, 145, DateTimeKind.Utc).AddTicks(5030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 7, 19, 46, 45, 289, DateTimeKind.Utc).AddTicks(3375));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Politics",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Profile",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Stores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Posted",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 7, 19, 46, 45, 289, DateTimeKind.Utc).AddTicks(3375),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 22, 17, 24, 38, 145, DateTimeKind.Utc).AddTicks(5030));
        }
    }
}
