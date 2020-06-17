using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Store.Migrations
{
    public partial class UserTownColumnDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderSendDate",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderSendDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            
        }
    }
}
