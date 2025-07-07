using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class paymentandstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a640f130-9b6b-4206-b81e-1b27a88c1568");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfac38c4-addf-4282-8e17-d6db6db0755b");

            migrationBuilder.AddColumn<string>(
                name: "MomoPaymentId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    MomoPaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.MomoPaymentId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_MomoPaymentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Payment");

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

            migrationBuilder.DropColumn(
                name: "MomoPaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a640f130-9b6b-4206-b81e-1b27a88c1568", null, "User", "USER" },
                    { "cfac38c4-addf-4282-8e17-d6db6db0755b", null, "Admin", "ADMIN" }
                });
        }
    }
}
