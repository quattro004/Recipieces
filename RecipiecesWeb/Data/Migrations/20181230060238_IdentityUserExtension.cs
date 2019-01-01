using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipiecesWeb.Data.Migrations
{
    public partial class IdentityUserExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "12b59b51-366d-40db-9f47-b300a7123990", "6d914ac4-2ca4-4f8b-a801-01ab8de878e0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "47e233c6-4f5f-4057-b9ac-921b74b34ae3", "f2264306-f23b-427d-9458-01a6adbb1af0" });

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f786af09-c40f-4afb-9595-381dc850708b", "d0edb8ae-cb70-4bad-98ec-0d8b95a7b1f5", "Admins", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0556b74f-dca3-479e-ac5a-1752c1d9e7b1", "d5c38e3e-49ed-4d3d-a486-6685494fb8c8", "Users", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0556b74f-dca3-479e-ac5a-1752c1d9e7b1", "d5c38e3e-49ed-4d3d-a486-6685494fb8c8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f786af09-c40f-4afb-9595-381dc850708b", "d0edb8ae-cb70-4bad-98ec-0d8b95a7b1f5" });

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12b59b51-366d-40db-9f47-b300a7123990", "6d914ac4-2ca4-4f8b-a801-01ab8de878e0", "Admins", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47e233c6-4f5f-4057-b9ac-921b74b34ae3", "f2264306-f23b-427d-9458-01a6adbb1af0", "Users", null });
        }
    }
}
