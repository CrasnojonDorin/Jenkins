using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class QtyInOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "OrderProducts");

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
    }
}
