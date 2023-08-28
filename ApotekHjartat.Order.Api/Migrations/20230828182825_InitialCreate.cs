using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApotekHjartat.Order.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_catalog.adminpack", ",,")
                .Annotation("Npgsql:PostgresExtension:pgagent.pgagent", ",,");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updatedAt = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_pkey", x => x.orderId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_pkey", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    orderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    orderId = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderItem_pkey", x => x.orderItemId);
                    table.ForeignKey(
                        name: "OrderItem_orderId_fkey",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "orderId");
                    table.ForeignKey(
                        name: "OrderItem_productId_fkey",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_orderId",
                table: "OrderItem",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_productId",
                table: "OrderItem",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
