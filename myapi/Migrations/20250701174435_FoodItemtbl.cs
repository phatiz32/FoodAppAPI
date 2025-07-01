using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace myapi.Migrations
{
    /// <inheritdoc />
    public partial class FoodItemtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4544e04a-f3be-4518-87d6-c3ad35169bdd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e03e2f7b-fe06-4195-ac28-07ba9e9a0965");

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c73df5e-ae34-493b-9c45-a593d3325326", null, "User", "USER" },
                    { "a31cb383-46d0-47d1-89f7-a60f0cc1cc4e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_CategoryId",
                table: "FoodItems",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");

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
                    { "4544e04a-f3be-4518-87d6-c3ad35169bdd", null, "Admin", "ADMIN" },
                    { "e03e2f7b-fe06-4195-ac28-07ba9e9a0965", null, "User", "USER" }
                });
        }
    }
}
