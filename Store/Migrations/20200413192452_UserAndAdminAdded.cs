using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class UserAndAdminAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address1", "Address2", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "GenderId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "PostCode", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "admin", null, "admin", "89451d05-8622-49aa-b87d-17ac312d58e1", "admin@store.com", true, "admin", 3, "admin", false, null, "ADMIN@STORE.COM", "ADMIN", "AQAAAAEAACcQAAAAENImR4+nsGIBgmfm/4u3QeLZUrBHvh1uOPIp52fkI2KKutPj5U01BVxO76rH7TObYQ==", "123456789", false, null, "51-627", "b60e83c9-5d4a-4c5d-8d5d-e11acdc2c120", false, "admin" },
                    { 2, 0, "user", null, "user", "aba18e9d-67ab-495d-b3be-8fc7f3b4d5be", "user@store.com", true, "user", 1, "user", false, null, "USER@STORE.COM", "USER", "AQAAAAEAACcQAAAAEJtj6ftDmAAcxw3aRotaX2G45fe3MfmJD1q4ai2KxJT5GTcwF4zq98kmVhUiDJcT2A==", "987654321", false, null, "51-627", "bcedfe57-4309-45d7-a70a-6bfa96a0cf15", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "16bb4f87-4b68-4abc-813c-2564d2826f54");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5a85338a-e4be-4183-b992-54f5a8456891");
        }
    }
}
