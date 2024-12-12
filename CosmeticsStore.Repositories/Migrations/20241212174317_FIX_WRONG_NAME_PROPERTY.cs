using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmeticsStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FIX_WRONG_NAME_PROPERTY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "RefreshToken",
                newName: "ExpirationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationTime",
                table: "RefreshToken",
                newName: "ExpiryDate");
        }
    }
}
