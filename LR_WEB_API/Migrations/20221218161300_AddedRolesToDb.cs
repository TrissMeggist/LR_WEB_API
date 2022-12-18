using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LRWEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5215ae9e-0364-4d89-870b-4f259aaa40d8", "11ec7748-a8e7-428d-b7db-4f0711279e71", "Administrator", "ADMINISTRATOR" },
                    { "845c9954-e376-4217-bdb2-fe313c380602", "9877a995-2a09-435c-8a75-6a076bf3199e", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5215ae9e-0364-4d89-870b-4f259aaa40d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "845c9954-e376-4217-bdb2-fe313c380602");
        }
    }
}
