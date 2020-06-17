using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class SeedDatabaseWithProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(15,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "ColorId", "Description", "Name", "PhotoPath", "Price", "SexId", "SizeId", "TypeId" },
                values: new object[,]
                {
                    { 1, 3, 2, "But z 97 roku!", "Air Max 97", "nikeairmax97.png", 399.99m, 3, 3, 1 },
                    { 2, 3, 1, "Klasyk noszony przez Forresta Gumpa!", "Cortez", "nikecortez.png", 199.99m, 1, 2, 1 },
                    { 3, 1, 6, "Kolejny model butów od najlepszego koszykarza w historii!", "30", "jordan30.png", 599.99m, 2, 4, 1 },
                    { 4, 4, 6, "Najpopularnieszy model bluzy Supreme!", "Bogo Red", "bogored.png", 999.99m, 1, 9, 2 },
                    { 5, 4, 1, "Świetny T-Shirt od Supreme!", "Buju Banton Tee", "bujubanton.png", 599.99m, 1, 8, 2 },
                    { 6, 4, 1, "Czarna czapka od Supreme!", "Camp Cap", "supremecapblack.png", 199.99m, 3, 7, 2 },
                    { 7, 2, 1, "Białe adidasy od Adidasa!", "Neo White", "adidasneowhite.png", 299.99m, 2, 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ed85cce2-6d54-408e-8916-5a1b5580b36a");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0e69e1db-6e89-4d36-a7f7-bba064a02b45");
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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dcc74487-ae05-4f5e-98ec-8cad28d8cbdd");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b2fde778-5e36-4e26-9978-91c4fc2c080c");
        }
    }
}
