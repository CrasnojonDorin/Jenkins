using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class SeedDataWithStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a214f263-5824-4d27-8df8-95ead7fe3b63");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4d189a03-6847-4f09-b3ee-df2fce43cd91");

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "IsLastElementOrdered", "Name", "ProductId", "Qty" },
                values: new object[,]
                {
                    { 1, false, "10", 1, 3 },
                    { 2, false, "11", 1, 2 },
                    { 3, false, "12", 1, 1 },
                    { 4, false, "S", 4, 2 },
                    { 5, false, "L", 4, 2 },
                    { 6, false, "XL", 4, 1 },
                    { 7, false, "9", 7, 1 },
                    { 8, false, "11", 2, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29fa1610-f6be-4f05-ac19-40986f92c07a", "AQAAAAEAACcQAAAAEEJuSKKffP66tvtH5hqiaWT6QDn+Ml1RFchFMT3NfRFuKdLOA54TWKSVdYbCyrA/uw==", "cea8647d-281d-405e-a1fa-b8234f096781" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36873a3f-0f65-4add-babb-9f7b1ae7a9fa", "AQAAAAEAACcQAAAAEFDzBhI8ta3GWtMjcnajwrtR06bpNpUsMnOC5KoRFYXHv3Bm4eFI7Lmq5ra563jQ8Q==", "a53af620-ee5a-4cf9-ada4-c5d485c6e6ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e575bc6a-c2f4-422a-ac1a-b43733e7ca88");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d42cd996-0fad-43c0-9351-341b26eac66f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89451d05-8622-49aa-b87d-17ac312d58e1", "AQAAAAEAACcQAAAAENImR4+nsGIBgmfm/4u3QeLZUrBHvh1uOPIp52fkI2KKutPj5U01BVxO76rH7TObYQ==", "b60e83c9-5d4a-4c5d-8d5d-e11acdc2c120" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aba18e9d-67ab-495d-b3be-8fc7f3b4d5be", "AQAAAAEAACcQAAAAEJtj6ftDmAAcxw3aRotaX2G45fe3MfmJD1q4ai2KxJT5GTcwF4zq98kmVhUiDJcT2A==", "bcedfe57-4309-45d7-a70a-6bfa96a0cf15" });
        }
    }
}
