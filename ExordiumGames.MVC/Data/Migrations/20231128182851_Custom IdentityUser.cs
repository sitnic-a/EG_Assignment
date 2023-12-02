using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExordiumGames.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Title" },
                values: new object[] { "cc5ec2a0-222e-4047-bd31-74ea1d84d006", "AQAAAAIAAYagAAAAEE9OzUAbi052DlYKnSLXurO1cFMc2M5t1/kqKsu4gXpCf5zC7iUJDb0VC/xBeoCKTw==", "285f53cb-23f0-4d29-997c-9e282b6bacb8", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77bd51d8-1b49-49ac-b612-388f43127f2f", "AQAAAAIAAYagAAAAECtJ3Y+lwM8iLRWxozOlyVL0J7eneIldPY9E9Z+RL1q+Sw6/ymd5nUd4pepwin1Umw==", "15601482-39da-46c4-99ea-6ff3b08e1900" });
        }
    }
}
