using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class InCartBoolAddedToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InCart",
                table: "Stock",
                nullable: false,
                defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InCart",
                table: "Stock");

        }
    }
}
