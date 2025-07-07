using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class FixMomoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fae9f80-7126-436a-9b63-94cdd04da21b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3021a064-9aa2-4c80-b715-a55c188ff813");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "290f426a-4d72-41da-956b-a7594138e0f0", null, "Admin", "ADMIN" },
                    { "4a5b8f18-b97e-4182-80b8-8acfcc80e8b4", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders",
                column: "MomoPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders");

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
                    { "1fae9f80-7126-436a-9b63-94cdd04da21b", null, "Admin", "ADMIN" },
                    { "3021a064-9aa2-4c80-b715-a55c188ff813", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders",
                column: "MomoPaymentId",
                unique: true,
                filter: "[MomoPaymentId] IS NOT NULL");
        }
    }
}
