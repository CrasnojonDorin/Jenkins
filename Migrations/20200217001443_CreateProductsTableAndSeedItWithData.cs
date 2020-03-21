using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class CreateProductsTableAndSeedItWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "ColorId", "Description", "Name", "PhotoPath", "Price", "SexId", "SizeId", "TypeId" },
                values: new object[] { 1, 3, 2, "But z 97 roku!", "Air Max 97", "nikeairmax97.jpg", 399.99000000000001, 3, 3, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "ColorId", "Description", "Name", "PhotoPath", "Price", "SexId", "SizeId", "TypeId" },
                values: new object[] { 2, 3, 1, "Klasyk noszony przez Forresta Gumpa!", "Air Max 90", "nikecortez.jpg", 199.99000000000001, 1, 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
