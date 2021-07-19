using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITService.Infrastructure.Migrations
{
    public partial class mt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("31f90867-cf63-491f-86dc-68a3968c62d9"), "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("42699092-fef8-4510-8c74-338c930be793"), "Employee" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("afcd1111-6907-43e7-b01b-eb4101ee59e5"), "Individual Customer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("31f90867-cf63-491f-86dc-68a3968c62d9"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("42699092-fef8-4510-8c74-338c930be793"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("afcd1111-6907-43e7-b01b-eb4101ee59e5"));
        }
    }
}
