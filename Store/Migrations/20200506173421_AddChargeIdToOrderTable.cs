using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddChargeIdToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargeId",
                table: "Orders",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeId",
                table: "Orders");

        }
    }
}
