using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentsDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "290f426a-4d72-41da-956b-a7594138e0f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a5b8f18-b97e-4182-80b8-8acfcc80e8b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "141e1ba0-a3a2-46c3-918c-8c47c0f9416d", null, "User", "USER" },
                    { "fed08d26-5b57-4ab7-a60b-d56f9d3db8ff", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "141e1ba0-a3a2-46c3-918c-8c47c0f9416d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fed08d26-5b57-4ab7-a60b-d56f9d3db8ff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "290f426a-4d72-41da-956b-a7594138e0f0", null, "Admin", "ADMIN" },
                    { "4a5b8f18-b97e-4182-80b8-8acfcc80e8b4", null, "User", "USER" }
                });
        }
    }
}
