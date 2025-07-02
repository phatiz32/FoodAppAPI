using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c73df5e-ae34-493b-9c45-a593d3325326");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a31cb383-46d0-47d1-89f7-a60f0cc1cc4e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a358204a-1d7f-441d-b589-903f0720d1c8", null, "Admin", "ADMIN" },
                    { "bbb9db46-7ba8-4edd-af5f-58c8af96c64a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a358204a-1d7f-441d-b589-903f0720d1c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbb9db46-7ba8-4edd-af5f-58c8af96c64a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c73df5e-ae34-493b-9c45-a593d3325326", null, "User", "USER" },
                    { "a31cb383-46d0-47d1-89f7-a60f0cc1cc4e", null, "Admin", "ADMIN" }
                });
        }
    }
}
