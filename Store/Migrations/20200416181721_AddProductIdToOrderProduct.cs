using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddProductIdToOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "440cdcb3-fa68-4302-bd7d-d2daf999ef95");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dafd95da-5783-41c1-b730-a74137b2bb93");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78b8c1e0-da8c-4213-86cb-e6d2778cbae0", "AQAAAAEAACcQAAAAEPoX0cLbm3yVlhqmRSc19zzd4Uh6/yOjF5r7yXckASqahFSE9533au5xtwl2aK3pHw==", "662d9587-7baf-43a8-9765-39450e7a1af2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d991460e-627b-4cd9-b014-256440b6b5aa", "AQAAAAEAACcQAAAAEOdnw4rzIrEj76iU5UKYsnAYmD1ii4m6vDjL0kndN0MqXmFHqM9AkhNDrko6gzxKWQ==", "2f9aca15-40a7-4374-9f2e-15b9727d9388" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f0ae06dd-e681-4d59-849f-1e799b59a083");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "38f4a8ed-3e51-4be2-a4b9-55cc5d74ea38");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d899fd01-4921-4064-b085-3cf3a50e4899", "AQAAAAEAACcQAAAAEK4DVmdbS2HXGgWYXFlHBX/o97ZLOCl5kDlrj6HdnlsEcH59pmT1AKlOlni/WftLbg==", "ad138d65-4634-4d3c-800a-eee8d158d2cb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e0a01b7-f84c-4433-bf1f-08061016c318", "AQAAAAEAACcQAAAAEBA+RthqThxO3cjQE0fqmyFEeA572BBNhd2Fan+1UAMQRHzS8Y5q9/0ub/FkPF1VZA==", "9e569036-9248-45b7-977d-d0526fd96b3c" });
        }
    }
}
