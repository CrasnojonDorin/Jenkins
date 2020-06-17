using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class SizeTableDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sizes_SizeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stock",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ef40105f-cf2a-4518-8e13-1a387450ea8f");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "90ba213d-80fa-4946-a505-1448a4731515");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stock");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a1489320-5c30-4a91-9421-5e9740db0cc4");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f55c15d1-4f9e-4b90-aaac-7bd4320a0fd0");

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, "10", 1 },
                    { 2, "11", 1 },
                    { 3, "12", 1 },
                    { 4, "13", 1 },
                    { 5, "14", 1 },
                    { 6, "S", 2 },
                    { 7, "M", 2 },
                    { 8, "L", 2 },
                    { 9, "XL", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "SizeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "SizeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "SizeId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "SizeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "SizeId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "SizeId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "SizeId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_TypeId",
                table: "Sizes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sizes_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
