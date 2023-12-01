using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExordiumGames.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelsdesignchangeAddedcolumnRetailerName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Retailers_RetailerId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "RetailerId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountDate",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "RetailerName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78df4086-83a8-40fe-b08f-cf021642aaaf", "AQAAAAIAAYagAAAAEByhcYn1jERGp+jdKRVX/pshNmVegNGfbvlxOzqADzR47d1+bhNyyw9QuFaVy36Ocw==", "a11ccb37-5b08-46cd-a1be-f92c8ae37f4c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Retailers_RetailerId",
                table: "Items",
                column: "RetailerId",
                principalTable: "Retailers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Retailers_RetailerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RetailerName",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "RetailerId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DiscountDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2629ba0f-f256-45b9-be9e-9a625a1767d3", "AQAAAAIAAYagAAAAECFRQi4R02/DVlTNxbKWP8KuA+eUV1V7QDRuFJ0aJRRVb2TKis9qK8m5u3+2GUrqFA==", "1a82c243-9649-4129-af6d-b93358225a1f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Retailers_RetailerId",
                table: "Items",
                column: "RetailerId",
                principalTable: "Retailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
