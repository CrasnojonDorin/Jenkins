using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhotoPath",
                value: "nikeairmax97.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhotoPath",
                value: "nikecortez.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhotoPath",
                value: "jordan30.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "PhotoPath",
                value: "bogored.png");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "ColorId", "Description", "Name", "PhotoPath", "Price", "SexId", "SizeId", "TypeId" },
                values: new object[,]
                {
                    { 5, 4, 1, "Świetny T-Shirt od Supreme!", "Buju Banton Tee", "bujubanton.png", 599.99000000000001, 1, 8, 2 },
                    { 6, 4, 1, "Czarna czapka od Supreme!", "Camp Cap", "supremecapblack.png", 199.99000000000001, 3, 7, 2 },
                    { 7, 2, 1, "Białe adidasy od Adidasa!", "Neo White", "adidasneowhite.png", 299.99000000000001, 2, 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhotoPath",
                value: "nikeairmax97.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhotoPath",
                value: "nikecortez.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhotoPath",
                value: "jordan30.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "PhotoPath",
                value: "bogored.jpg");
        }
    }
}
