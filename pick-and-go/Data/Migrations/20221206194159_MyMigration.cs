using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickAndGo.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    firstName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    emailAddress = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    adminUser = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    dateSignedUp = table.Column<DateTime>(type: "date", nullable: true),
                    dateLastOrdered = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "DietaryType",
                columns: table => new
                {
                    dietaryID = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    dietaryName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    dietaryImage = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DietaryT__9B5E3E4CF087F04B", x => x.dietaryID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productID = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    basePrice = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    image = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ingredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    categoryID = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    inStock = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ingredientID);
                    table.ForeignKey(
                        name: "FK__Ingredien__categ__628FA481",
                        column: x => x.categoryID,
                        principalTable: "Category",
                        principalColumn: "categoryID");
                });

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "date", nullable: true),
                    orderValue = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    pickupTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    orderStatus = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderHea__0809337DA99DA31A", x => x.orderID);
                    table.ForeignKey(
                        name: "FK__OrderHead__custo__656C112C",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID");
                });

            migrationBuilder.CreateTable(
                name: "Customer_DietaryType",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false),
                    dietaryID = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_DietaryType", x => new { x.customerID, x.dietaryID });
                    table.ForeignKey(
                        name: "FK__Customer___custo__778AC167",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK__Customer___dieta__787EE5A0",
                        column: x => x.dietaryID,
                        principalTable: "DietaryType",
                        principalColumn: "dietaryID");
                });

            migrationBuilder.CreateTable(
                name: "Ingredient_DietaryType",
                columns: table => new
                {
                    ingredientID = table.Column<int>(type: "int", nullable: false),
                    dietaryID = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient_DietaryType", x => new { x.ingredientID, x.dietaryID });
                    table.ForeignKey(
                        name: "FK__Ingredien__dieta__7C4F7684",
                        column: x => x.dietaryID,
                        principalTable: "DietaryType",
                        principalColumn: "dietaryID");
                    table.ForeignKey(
                        name: "FK__Ingredien__ingre__7B5B524B",
                        column: x => x.ingredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ingredientID");
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false),
                    lineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.orderID, x.lineID });
                    table.ForeignKey(
                        name: "FK__OrderLine__order__68487DD7",
                        column: x => x.orderID,
                        principalTable: "OrderHeader",
                        principalColumn: "orderID");
                    table.ForeignKey(
                        name: "FK__OrderLine__produ__693CA210",
                        column: x => x.productID,
                        principalTable: "Product",
                        principalColumn: "productID");
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false),
                    orderID = table.Column<int>(type: "int", nullable: false),
                    lineID = table.Column<int>(type: "int", nullable: false),
                    favoriteName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => new { x.customerID, x.orderID, x.lineID });
                    table.ForeignKey(
                        name: "FK__Favorite__72C60C4A",
                        columns: x => new { x.orderID, x.lineID },
                        principalTable: "OrderLine",
                        principalColumns: new[] { "orderID", "lineID" });
                    table.ForeignKey(
                        name: "FK__Favorite__custom__70DDC3D8",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK__Favorite__orderI__71D1E811",
                        column: x => x.orderID,
                        principalTable: "OrderHeader",
                        principalColumn: "orderID");
                });

            migrationBuilder.CreateTable(
                name: "LineIngredient",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false),
                    lineID = table.Column<int>(type: "int", nullable: false),
                    ingredientID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineIngredient", x => new { x.orderID, x.lineID, x.ingredientID });
                    table.ForeignKey(
                        name: "FK__LineIngre__ingre__6C190EBB",
                        column: x => x.ingredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ingredientID");
                    table.ForeignKey(
                        name: "FK__LineIngre__order__6D0D32F4",
                        column: x => x.orderID,
                        principalTable: "OrderHeader",
                        principalColumn: "orderID");
                    table.ForeignKey(
                        name: "FK__LineIngredient__6E01572D",
                        columns: x => new { x.orderID, x.lineID },
                        principalTable: "OrderLine",
                        principalColumns: new[] { "orderID", "lineID" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DietaryType_dietaryID",
                table: "Customer_DietaryType",
                column: "dietaryID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_orderID_lineID",
                table: "Favorite",
                columns: new[] { "orderID", "lineID" });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_categoryID",
                table: "Ingredient",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_DietaryType_dietaryID",
                table: "Ingredient_DietaryType",
                column: "dietaryID");

            migrationBuilder.CreateIndex(
                name: "IX_LineIngredient_ingredientID",
                table: "LineIngredient",
                column: "ingredientID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_customerID",
                table: "OrderHeader",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_productID",
                table: "OrderLine",
                column: "productID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Customer_DietaryType");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Ingredient_DietaryType");

            migrationBuilder.DropTable(
                name: "LineIngredient");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DietaryType");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
