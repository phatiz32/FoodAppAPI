using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class morefuture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_MomoPaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37e52b2e-e909-4fd0-98a5-98622c4091ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab22c22b-4f36-4665-b490-c78c34828312");

            migrationBuilder.AlterColumn<string>(
                name: "MomoPaymentId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_MomoPaymentId",
                table: "Orders",
                column: "MomoPaymentId",
                principalTable: "Payment",
                principalColumn: "MomoPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_MomoPaymentId",
                table: "Orders");

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

            migrationBuilder.AlterColumn<string>(
                name: "MomoPaymentId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37e52b2e-e909-4fd0-98a5-98622c4091ea", null, "User", "USER" },
                    { "ab22c22b-4f36-4665-b490-c78c34828312", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MomoPaymentId",
                table: "Orders",
                column: "MomoPaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_MomoPaymentId",
                table: "Orders",
                column: "MomoPaymentId",
                principalTable: "Payment",
                principalColumn: "MomoPaymentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
