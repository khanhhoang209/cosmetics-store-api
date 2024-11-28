using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CosmeticsStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SEEDING_USER_ROLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "manager", "MANAGER" },
                    { "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "customer", "CUSTOMER" },
                    { "51ef7e08-ff07-459b-8c55-c7ebac505103", "51ef7e08-ff07-459b-8c55-c7ebac505103", "staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3631e38b-60dd-4d1a-af7f-a26f21c2ef82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37a7c5df-4898-4fd4-8e5f-d2abd4b57520");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51ef7e08-ff07-459b-8c55-c7ebac505103");
        }
    }
}
