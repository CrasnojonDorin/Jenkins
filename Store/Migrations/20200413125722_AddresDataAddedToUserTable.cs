using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddresDataAddedToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

        }
    }
}
