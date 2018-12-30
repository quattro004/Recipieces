using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipiecesWeb.Data.Migrations
{
    public partial class IdentityRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12b59b51-366d-40db-9f47-b300a7123990", "6d914ac4-2ca4-4f8b-a801-01ab8de878e0", "Admins", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47e233c6-4f5f-4057-b9ac-921b74b34ae3", "f2264306-f23b-427d-9458-01a6adbb1af0", "Users", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "12b59b51-366d-40db-9f47-b300a7123990", "6d914ac4-2ca4-4f8b-a801-01ab8de878e0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "47e233c6-4f5f-4057-b9ac-921b74b34ae3", "f2264306-f23b-427d-9458-01a6adbb1af0" });
        }
    }
}
