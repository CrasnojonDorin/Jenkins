using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class XD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "Users");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
