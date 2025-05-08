using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tenkiu.api.order.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exchange_Rate",
                columns: table => new
                {
                    ID_Exchange_Rate = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Currency_Origen = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Currency_Destination = table.Column<int>(type: "int(11)", nullable: false),
                    Exchange_Rate = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Exchange_Rate);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Delivery_season = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Delivery_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ID_Client = table.Column<int>(type: "int(11)", nullable: false),
                    hash = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Order);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Shipping_Types",
                columns: table => new
                {
                    ID_Shipping_Type = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Web_site = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Shipping_Type);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Status_Order",
                columns: table => new
                {
                    ID_Status_Order = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Status_Order);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Status_Order_Detail",
                columns: table => new
                {
                    ID_Status_Order_Detail = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Status_Order_Detail);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Order_Details",
                columns: table => new
                {
                    ID_Order_Details = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Product = table.Column<int>(type: "int(11)", nullable: false),
                    Listed_Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency_Listed = table.Column<int>(type: "int(11)", nullable: false),
                    Purchase_Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Purchase_Price_Tax = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency_Purchase = table.Column<int>(type: "int(11)", nullable: false),
                    Sell_Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency_Sell = table.Column<int>(type: "int(11)", nullable: false),
                    Quantity = table.Column<int>(type: "int(11)", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Order_Details);
                    table.ForeignKey(
                        name: "Order_Details_ibfk_1",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Payment_History",
                columns: table => new
                {
                    ID_Payment_History = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency = table.Column<int>(type: "int(11)", nullable: false),
                    ID_User = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false),
                    Payment_Type = table.Column<int>(type: "int(11)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Payment_History);
                    table.ForeignKey(
                        name: "Payment_History_ibfk_1",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    ID_Shipping = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Shipping_Type = table.Column<int>(type: "int(11)", nullable: false),
                    Guide_Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date_Shipping = table.Column<DateOnly>(type: "date", nullable: true),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Shipping);
                    table.ForeignKey(
                        name: "Shipping_ibfk_1",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Relation_Order_Status",
                columns: table => new
                {
                    ID_Relation_Order_Status = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Status_Order = table.Column<int>(type: "int(11)", nullable: false),
                    Date_Relation = table.Column<DateOnly>(type: "date", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Relation_Order_Status);
                    table.ForeignKey(
                        name: "Relation_Order_Status_ibfk_1",
                        column: x => x.ID_Order,
                        principalTable: "Orders",
                        principalColumn: "ID_Order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relation_Order_Status_ibfk_2",
                        column: x => x.ID_Status_Order,
                        principalTable: "Status_Order",
                        principalColumn: "ID_Status_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Relation_Order_Details_Status",
                columns: table => new
                {
                    ID_Relation_ODS = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Order_Details = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Status_Product = table.Column<int>(type: "int(11)", nullable: false),
                    Date_Relation = table.Column<DateOnly>(type: "date", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Relation_ODS);
                    table.ForeignKey(
                        name: "Relation_Order_Details_Status_ibfk_1",
                        column: x => x.ID_Order_Details,
                        principalTable: "Order_Details",
                        principalColumn: "ID_Order_Details",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relation_Order_Details_Status_ibfk_2",
                        column: x => x.ID_Status_Product,
                        principalTable: "Status_Order_Detail",
                        principalColumn: "ID_Status_Order_Detail",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.InsertData(
                table: "Status_Order",
                columns: new[] { "ID_Status_Order", "Created_by", "Created_dt", "Description", "Modified_by", "Modified_dt", "Name" },
                values: new object[,]
                {
                  { 1, 0, DateTime.UtcNow, "Order has been created in the system.", null, null, "Created" },
                  { 2, 0, DateTime.UtcNow, "Order initialization is complete and awaiting confirmation.", null, null, "Initialized" },
                  { 3, 0, DateTime.UtcNow, "Order has been confirmed by the customer or system.", null, null, "Confirmed" },
                  { 4, 0, DateTime.UtcNow, "Order is being processed and items are being prepared.", null, null, "Processing" },
                  { 5, 0, DateTime.UtcNow, "Order items have been packed and are ready for shipping.", null, null, "Packed" },
                  { 6, 0, DateTime.UtcNow, "Order has been shipped to the delivery address.", null, null, "Shipped" },
                  { 7, 0, DateTime.UtcNow, "Order is out for delivery with the carrier.", null, null, "OutForDelivery" },
                  { 8, 0, DateTime.UtcNow, "Order has been delivered to the recipient.", null, null, "Delivered" },
                  { 9, 0, DateTime.UtcNow, "Order has been cancelled and will not be fulfilled.", null, null, "Cancelled" },
                  { 10, 0, DateTime.UtcNow, "Order has been returned by the customer.", null, null, "Returned" },
                  { 11, 0, DateTime.UtcNow, "Order payment has been refunded.", null, null, "Refunded" }
                });

            migrationBuilder.InsertData(
                table: "Status_Order_Detail",
                columns: new[] { "ID_Status_Order_Detail", "Created_by", "Created_dt", "Description", "Modified_by", "Modified_dt", "Name" },
                values: new object[,]
                {
                  { 1, 0, DateTime.UtcNow, "Product is pending purchase.", null, null, "Pending" },
                  { 2, 0, DateTime.UtcNow, "Product purchase has been completed.", null, null, "Purchased" },
                  { 3, 0, DateTime.UtcNow, "Product is being prepared for shipment.", null, null, "InPreparation" },
                  { 4, 0, DateTime.UtcNow, "Product has been packaged.", null, null, "Packed" },
                  { 5, 0, DateTime.UtcNow, "Product is in transit to the destination.", null, null, "InTransit" },
                  { 6, 0, DateTime.UtcNow, "Product has been delivered to the recipient.", null, null, "Delivered" },
                  { 7, 0, DateTime.UtcNow, "Product order has been cancelled.", null, null, "Cancelled" },
                  { 8, 0, DateTime.UtcNow, "Product has been returned by the customer.", null, null, "Returned" },
                  { 9, 0, DateTime.UtcNow, "Product is on backorder due to stock unavailability.", null, null, "Backordered" }
                });

            migrationBuilder.CreateIndex(
                name: "ID_Order",
                table: "Order_Details",
                column: "ID_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Order1",
                table: "Payment_History",
                column: "ID_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Order_Details",
                table: "Relation_Order_Details_Status",
                column: "ID_Order_Details");

            migrationBuilder.CreateIndex(
                name: "ID_Status_Product",
                table: "Relation_Order_Details_Status",
                column: "ID_Status_Product");

            migrationBuilder.CreateIndex(
                name: "ID_Order2",
                table: "Relation_Order_Status",
                column: "ID_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Status_Order",
                table: "Relation_Order_Status",
                column: "ID_Status_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Order3",
                table: "Shipping",
                column: "ID_Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchange_Rate");

            migrationBuilder.DropTable(
                name: "Payment_History");

            migrationBuilder.DropTable(
                name: "Relation_Order_Details_Status");

            migrationBuilder.DropTable(
                name: "Relation_Order_Status");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Shipping_Types");

            migrationBuilder.DropTable(
                name: "Order_Details");

            migrationBuilder.DropTable(
                name: "Status_Order_Detail");

            migrationBuilder.DropTable(
                name: "Status_Order");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
