using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySet",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySet", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductSet",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSet", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "RoleSet",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSet", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserSet",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleModelUserTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSet", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserSet_RoleSet_RoleModelUserTypeId",
                        column: x => x.RoleModelUserTypeId,
                        principalTable: "RoleSet",
                        principalColumn: "UserTypeId");
                });

            migrationBuilder.CreateTable(
                name: "CartSet",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    productname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModelUserId = table.Column<int>(type: "int", nullable: true),
                    ProductModel_ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSet", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_CartSet_ProductSet_ProductModel_ProductId",
                        column: x => x.ProductModel_ProductId,
                        principalTable: "ProductSet",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartSet_UserSet_UserModelUserId",
                        column: x => x.UserModelUserId,
                        principalTable: "UserSet",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderSet",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<float>(type: "real", nullable: false),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartModelCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSet", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderSet_CartSet_CartModelCartId",
                        column: x => x.CartModelCartId,
                        principalTable: "CartSet",
                        principalColumn: "CartId");
                });

            migrationBuilder.CreateTable(
                name: "PreviousOrdersSet",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<float>(type: "real", nullable: false),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartModelCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousOrdersSet", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_PreviousOrdersSet_CartSet_CartModelCartId",
                        column: x => x.CartModelCartId,
                        principalTable: "CartSet",
                        principalColumn: "CartId");
                });

            migrationBuilder.CreateTable(
                name: "WalletSet",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentBalance = table.Column<int>(type: "int", nullable: false),
                    UserModel_UserId = table.Column<int>(type: "int", nullable: false),
                    CartModelCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletSet", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_WalletSet_CartSet_CartModelCartId",
                        column: x => x.CartModelCartId,
                        principalTable: "CartSet",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK_WalletSet_UserSet_UserModel_UserId",
                        column: x => x.UserModel_UserId,
                        principalTable: "UserSet",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartSet_ProductModel_ProductId",
                table: "CartSet",
                column: "ProductModel_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartSet_UserModelUserId",
                table: "CartSet",
                column: "UserModelUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSet_CartModelCartId",
                table: "OrderSet",
                column: "CartModelCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousOrdersSet_CartModelCartId",
                table: "PreviousOrdersSet",
                column: "CartModelCartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_RoleModelUserTypeId",
                table: "UserSet",
                column: "RoleModelUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletSet_CartModelCartId",
                table: "WalletSet",
                column: "CartModelCartId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletSet_UserModel_UserId",
                table: "WalletSet",
                column: "UserModel_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySet");

            migrationBuilder.DropTable(
                name: "OrderSet");

            migrationBuilder.DropTable(
                name: "PreviousOrdersSet");

            migrationBuilder.DropTable(
                name: "WalletSet");

            migrationBuilder.DropTable(
                name: "CartSet");

            migrationBuilder.DropTable(
                name: "ProductSet");

            migrationBuilder.DropTable(
                name: "UserSet");

            migrationBuilder.DropTable(
                name: "RoleSet");
        }
    }
}
