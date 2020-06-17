using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class OrderProductsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "InCart",
                table: "Stock");

            migrationBuilder.AddColumn<bool>(
                name: "IsLastElementOrdered",
                table: "Stock",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderProducts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "StockId", "OrderId" });


            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Stock_StockId",
                table: "OrderProducts",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Stock_StockId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "IsLastElementOrdered",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<bool>(
                name: "InCart",
                table: "Stock",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "ProductId", "OrderId" });


            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
