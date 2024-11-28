using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmeticsStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ADDED_PAYMENT_METHOD_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Method",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Method", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Method_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Method",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_MethodId",
                table: "Payment",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Method");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryId");
        }
    }
}
