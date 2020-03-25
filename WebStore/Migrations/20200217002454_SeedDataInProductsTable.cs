using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class SeedDataInProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Red");

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Inny" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "ColorId", "Description", "Name", "PhotoPath", "Price", "SexId", "SizeId", "TypeId" },
                values: new object[,]
                {
                    { 3, 1, 6, "Kolejny model butów od najlepszego koszykarza w historii!", "30", "jordan30.jpg", 599.99000000000001, 2, 4, 1 },
                    { 4, 4, 6, "Najpopularnieszy model bluzy Supreme!", "Bogo Red", "bogored.jpg", 999.99000000000001, 1, 9, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Inny");
        }
    }
}
