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
                name: "Buy_Order",
                columns: table => new
                {
                    ID_Buy_Order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Store = table.Column<int>(type: "int(11)", nullable: false),
                    Purchase_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Base_Currency_Id = table.Column<int>(type: "int(11)", nullable: false),
                    Converted_Currency_Id = table.Column<int>(type: "int(11)", nullable: false),
                    Exchange_Rate = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Buy_Order);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Delivery_Periods",
                columns: table => new
                {
                    ID_Delivery_Period = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Period_Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Is_Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Start_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Delivery_Period);
                })
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shipping_Types",
                columns: table => new
                {
                    ID_Shipping_Type = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Web_site = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Status_Order",
                columns: table => new
                {
                    ID_Status_Order = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Status_Order_Detail",
                columns: table => new
                {
                    ID_Status_Order_Detail = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buy_Order_Details",
                columns: table => new
                {
                    ID_Buy_Order_Details = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Buy_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Product = table.Column<int>(type: "int(11)", nullable: false),
                    Purchase_Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Purchase_Price_Tax = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency_Purchase = table.Column<int>(type: "int(11)", nullable: false),
                    Quantity = table.Column<int>(type: "int(11)", nullable: false),
                    Converted_Purchase_Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ID_Currency_Converted_Purchase = table.Column<int>(type: "int(11)", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Buy_Order_Details);
                    table.ForeignKey(
                        name: "Buy_Order_Details_ibfk_1",
                        column: x => x.ID_Buy_Order,
                        principalTable: "Buy_Order",
                        principalColumn: "ID_Buy_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sell_Order",
                columns: table => new
                {
                    ID_Sell_Order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Delivery_Period = table.Column<int>(type: "int(11)", nullable: false),
                    Delivery_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ID_Client = table.Column<int>(type: "int(11)", nullable: false),
                    hash = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total_Sell_Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Base_Currency_Id = table.Column<int>(type: "int(11)", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Sell_Order);
                    table.ForeignKey(
                        name: "Delivery_Periods_ibfk_1",
                        column: x => x.ID_Delivery_Period,
                        principalTable: "Delivery_Periods",
                        principalColumn: "ID_Delivery_Period",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                        principalTable: "Sell_Order",
                        principalColumn: "ID_Sell_Order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relation_Order_Status_ibfk_2",
                        column: x => x.ID_Status_Order,
                        principalTable: "Status_Order",
                        principalColumn: "ID_Status_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sell_Order_Details",
                columns: table => new
                {
                    ID_Sell_Order_Details = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Sell_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Product = table.Column<int>(type: "int(11)", nullable: false),
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
                    table.PrimaryKey("PRIMARY", x => x.ID_Sell_Order_Details);
                    table.ForeignKey(
                        name: "Sell_Order_Details_ibfk_1",
                        column: x => x.ID_Sell_Order,
                        principalTable: "Sell_Order",
                        principalColumn: "ID_Sell_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sell_Order_Payment_History",
                columns: table => new
                {
                    ID_Payment_History = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ID_Currency = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Client = table.Column<int>(type: "int(11)", nullable: false),
                    Payment_Direction = table.Column<int>(type: "int(11)", nullable: false),
                    Payment_Reason = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Sell_Order = table.Column<int>(type: "int(11)", nullable: false),
                    Payment_Type = table.Column<int>(type: "int(11)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payment_Date = table.Column<DateOnly>(type: "date", nullable: false),
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
                        column: x => x.ID_Sell_Order,
                        principalTable: "Sell_Order",
                        principalColumn: "ID_Sell_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    ID_Shipping = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Order = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Shipping_Type = table.Column<int>(type: "int(11)", nullable: false),
                    Guide_Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
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
                        principalTable: "Sell_Order",
                        principalColumn: "ID_Sell_Order",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BuySell_Allocation",
                columns: table => new
                {
                    ID_Allocation = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Buy_Order_Detail = table.Column<int>(type: "int(11)", nullable: false),
                    ID_Sell_Order_Detail = table.Column<int>(type: "int(11)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<int>(type: "int(11)", nullable: false),
                    Created_dt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modified_by = table.Column<int>(type: "int(11)", nullable: true),
                    Modified_dt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Allocation);
                    table.ForeignKey(
                        name: "BuySell_Allocation_ibfk_1",
                        column: x => x.ID_Buy_Order_Detail,
                        principalTable: "Buy_Order_Details",
                        principalColumn: "ID_Buy_Order_Details",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BuySell_Allocation_ibfk_2",
                        column: x => x.ID_Sell_Order_Detail,
                        principalTable: "Sell_Order_Details",
                        principalColumn: "ID_Sell_Order_Details",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                        principalTable: "Sell_Order_Details",
                        principalColumn: "ID_Sell_Order_Details",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relation_Order_Details_Status_ibfk_2",
                        column: x => x.ID_Status_Product,
                        principalTable: "Status_Order_Detail",
                        principalColumn: "ID_Status_Order_Detail",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Status_Order",
                columns: new[] { "ID_Status_Order", "Created_by", "Created_dt", "Description", "Modified_by", "Modified_dt", "Name" },
                values: new object[,]
                {
                    { 1, 0, DateTime.Now, "Order has been created in the system.", null, null, "Created" },
                    { 2, 0, DateTime.Now, "Order initialization is complete and awaiting confirmation.", null, null, "Initialized" },
                    { 3, 0, DateTime.Now, "Order has been confirmed by the customer or system.", null, null, "Confirmed" },
                    { 4, 0, DateTime.Now, "Order is being processed and items are being prepared.", null, null, "Processing" },
                    { 5, 0, DateTime.Now, "Order items have been packed and are ready for shipping.", null, null, "Packed" },
                    { 6, 0, DateTime.Now, "Order has been shipped to the delivery address.", null, null, "Shipped" },
                    { 7, 0, DateTime.Now, "Order is out for delivery with the carrier.", null, null, "OutForDelivery" },
                    { 8, 0, DateTime.Now, "Order has been delivered to the recipient.", null, null, "Delivered" },
                    { 9, 0, DateTime.Now, "Order has been cancelled and will not be fulfilled.", null, null, "Cancelled" },
                    { 10, 0, DateTime.Now, "Order has been returned by the customer.", null, null, "Returned" },
                    { 11, 0, DateTime.Now, "Order payment has been refunded.", null, null, "Refunded" },
                });

            migrationBuilder.InsertData(
                table: "Status_Order_Detail",
                columns: new[] { "ID_Status_Order_Detail", "Created_by", "Created_dt", "Description", "Modified_by", "Modified_dt", "Name" },
                values: new object[,]
                {
                    { 1, 0, DateTime.Now, "Product is pending purchase.", null, null, "Pending" },
                    { 2, 0, DateTime.Now, "Product purchase has been completed.", null, null, "Purchased" },
                    { 3, 0, DateTime.Now, "Product is being prepared for shipment.", null, null, "InPreparation" },
                    { 4, 0, DateTime.Now, "Product has been packaged.", null, null, "Packed" },
                    { 5, 0, DateTime.Now, "Product is in transit to the destination.", null, null, "InTransit" },
                    { 6, 0, DateTime.Now, "Product has been delivered to the recipient.", null, null, "Delivered" },
                    { 7, 0, DateTime.Now, "Product order has been cancelled.", null, null, "Cancelled" },
                    { 8, 0, DateTime.Now, "Product has been returned by the customer.", null, null, "Returned" },
                    { 9, 0, DateTime.Now, "Product is on backorder due to stock unavailability.", null, null, "Backordered" },
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buy_Order_Details_ID_Buy_Order",
                table: "Buy_Order_Details",
                column: "ID_Buy_Order");

            migrationBuilder.CreateIndex(
                name: "IX_BuySell_Allocation_ID_Buy_Order_Detail",
                table: "BuySell_Allocation",
                column: "ID_Buy_Order_Detail");

            migrationBuilder.CreateIndex(
                name: "IX_BuySell_Allocation_ID_Sell_Order_Detail",
                table: "BuySell_Allocation",
                column: "ID_Sell_Order_Detail");

            migrationBuilder.CreateIndex(
                name: "ID_Order_Details",
                table: "Relation_Order_Details_Status",
                column: "ID_Order_Details");

            migrationBuilder.CreateIndex(
                name: "ID_Status_Product",
                table: "Relation_Order_Details_Status",
                column: "ID_Status_Product");

            migrationBuilder.CreateIndex(
                name: "ID_Order1",
                table: "Relation_Order_Status",
                column: "ID_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Status_Order",
                table: "Relation_Order_Status",
                column: "ID_Status_Order");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Order_ID_Delivery_Period",
                table: "Sell_Order",
                column: "ID_Delivery_Period");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Order_Details_ID_Sell_Order",
                table: "Sell_Order_Details",
                column: "ID_Sell_Order");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Order_Payment_History_ID_Sell_Order",
                table: "Sell_Order_Payment_History",
                column: "ID_Sell_Order");

            migrationBuilder.CreateIndex(
                name: "ID_Order",
                table: "Shipping",
                column: "ID_Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuySell_Allocation");

            migrationBuilder.DropTable(
                name: "Exchange_Rate");

            migrationBuilder.DropTable(
                name: "Relation_Order_Details_Status");

            migrationBuilder.DropTable(
                name: "Relation_Order_Status");

            migrationBuilder.DropTable(
                name: "Sell_Order_Payment_History");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Shipping_Types");

            migrationBuilder.DropTable(
                name: "Buy_Order_Details");

            migrationBuilder.DropTable(
                name: "Sell_Order_Details");

            migrationBuilder.DropTable(
                name: "Status_Order_Detail");

            migrationBuilder.DropTable(
                name: "Status_Order");

            migrationBuilder.DropTable(
                name: "Buy_Order");

            migrationBuilder.DropTable(
                name: "Sell_Order");

            migrationBuilder.DropTable(
                name: "Delivery_Periods");
        }
    }
}
