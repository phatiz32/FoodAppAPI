using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class Categorytbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47b36958-5113-429b-b13f-c1a7f7a90647");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78cc4a3c-0a91-43c5-8e95-5315fe8dae17");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "911f2698-0fd5-4328-a16a-74a2e5fa27aa", null, "User", "USER" },
                    { "a95cd4a7-ec17-4999-a9c8-cf9a288e264f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "911f2698-0fd5-4328-a16a-74a2e5fa27aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a95cd4a7-ec17-4999-a9c8-cf9a288e264f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47b36958-5113-429b-b13f-c1a7f7a90647", null, "Admin", "ADMIN" },
                    { "78cc4a3c-0a91-43c5-8e95-5315fe8dae17", null, "User", "USER" }
                });
        }
    }
}
