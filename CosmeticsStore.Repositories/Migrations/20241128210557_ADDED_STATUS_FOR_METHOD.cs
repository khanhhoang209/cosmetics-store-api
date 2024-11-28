using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmeticsStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ADDED_STATUS_FOR_METHOD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Method",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Method");
        }
    }
}
