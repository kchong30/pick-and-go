using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickAndGo.Migrations.PickAndGo
{
    public partial class _0217 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Customer___custo__778AC167",
                table: "Customer_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Customer___dieta__787EE5A0",
                table: "Customer_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__72C60C4A",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__custom__70DDC3D8",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__orderI__71D1E811",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__categ__628FA481",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__dieta__7C4F7684",
                table: "Ingredient_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__ingre__7B5B524B",
                table: "Ingredient_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngre__ingre__6C190EBB",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngre__order__6D0D32F4",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngredient__6E01572D",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderHead__custo__656C112C",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderLine__order__68487DD7",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderLine__produ__693CA210",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderHea__0809337DA99DA31A",
                table: "OrderHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DietaryT__9B5E3E4CF087F04B",
                table: "DietaryType");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Product",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lineStatus",
                table: "OrderLine",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true,
                oldDefaultValueSql: "('O')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pickupTime",
                table: "OrderHeader",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "orderStatus",
                table: "OrderHeader",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "OrderHeader",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "paymentDate",
                table: "OrderHeader",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymentID",
                table: "OrderHeader",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymentType",
                table: "OrderHeader",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dietaryImage",
                table: "DietaryType",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderHea__0809337D852D03AF",
                table: "OrderHeader",
                column: "orderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DietaryT__9B5E3E4C03561F59",
                table: "DietaryType",
                column: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Customer___custo__7226EDCC",
                table: "Customer_DietaryType",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__Customer___dieta__731B1205",
                table: "Customer_DietaryType",
                column: "dietaryID",
                principalTable: "DietaryType",
                principalColumn: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__6D6238AF",
                table: "Favorite",
                columns: new[] { "orderID", "lineID" },
                principalTable: "OrderLine",
                principalColumns: new[] { "orderID", "lineID" });

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__custom__6B79F03D",
                table: "Favorite",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__orderI__6C6E1476",
                table: "Favorite",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__categ__5D2BD0E6",
                table: "Ingredient",
                column: "categoryID",
                principalTable: "Category",
                principalColumn: "categoryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__dieta__76EBA2E9",
                table: "Ingredient_DietaryType",
                column: "dietaryID",
                principalTable: "DietaryType",
                principalColumn: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__ingre__75F77EB0",
                table: "Ingredient_DietaryType",
                column: "ingredientID",
                principalTable: "Ingredient",
                principalColumn: "ingredientID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngre__ingre__66B53B20",
                table: "LineIngredient",
                column: "ingredientID",
                principalTable: "Ingredient",
                principalColumn: "ingredientID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngre__order__67A95F59",
                table: "LineIngredient",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngredient__689D8392",
                table: "LineIngredient",
                columns: new[] { "orderID", "lineID" },
                principalTable: "OrderLine",
                principalColumns: new[] { "orderID", "lineID" });

            migrationBuilder.AddForeignKey(
                name: "FK__OrderHead__custo__60083D91",
                table: "OrderHeader",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderLine__order__62E4AA3C",
                table: "OrderLine",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderLine__produ__63D8CE75",
                table: "OrderLine",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Customer___custo__7226EDCC",
                table: "Customer_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Customer___dieta__731B1205",
                table: "Customer_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__6D6238AF",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__custom__6B79F03D",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorite__orderI__6C6E1476",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__categ__5D2BD0E6",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__dieta__76EBA2E9",
                table: "Ingredient_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__ingre__75F77EB0",
                table: "Ingredient_DietaryType");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngre__ingre__66B53B20",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngre__order__67A95F59",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__LineIngredient__689D8392",
                table: "LineIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderHead__custo__60083D91",
                table: "OrderHeader");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderLine__order__62E4AA3C",
                table: "OrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderLine__produ__63D8CE75",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK__OrderHea__0809337D852D03AF",
                table: "OrderHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DietaryT__9B5E3E4C03561F59",
                table: "DietaryType");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "paymentDate",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "paymentID",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "paymentType",
                table: "OrderHeader");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "Product",
                type: "image",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lineStatus",
                table: "OrderLine",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                defaultValueSql: "('O')",
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "pickupTime",
                table: "OrderHeader",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "orderStatus",
                table: "OrderHeader",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<byte[]>(
                name: "dietaryImage",
                table: "DietaryType",
                type: "image",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__OrderHea__0809337DA99DA31A",
                table: "OrderHeader",
                column: "orderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DietaryT__9B5E3E4CF087F04B",
                table: "DietaryType",
                column: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Customer___custo__778AC167",
                table: "Customer_DietaryType",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__Customer___dieta__787EE5A0",
                table: "Customer_DietaryType",
                column: "dietaryID",
                principalTable: "DietaryType",
                principalColumn: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__72C60C4A",
                table: "Favorite",
                columns: new[] { "orderID", "lineID" },
                principalTable: "OrderLine",
                principalColumns: new[] { "orderID", "lineID" });

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__custom__70DDC3D8",
                table: "Favorite",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorite__orderI__71D1E811",
                table: "Favorite",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__categ__628FA481",
                table: "Ingredient",
                column: "categoryID",
                principalTable: "Category",
                principalColumn: "categoryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__dieta__7C4F7684",
                table: "Ingredient_DietaryType",
                column: "dietaryID",
                principalTable: "DietaryType",
                principalColumn: "dietaryID");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__ingre__7B5B524B",
                table: "Ingredient_DietaryType",
                column: "ingredientID",
                principalTable: "Ingredient",
                principalColumn: "ingredientID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngre__ingre__6C190EBB",
                table: "LineIngredient",
                column: "ingredientID",
                principalTable: "Ingredient",
                principalColumn: "ingredientID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngre__order__6D0D32F4",
                table: "LineIngredient",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__LineIngredient__6E01572D",
                table: "LineIngredient",
                columns: new[] { "orderID", "lineID" },
                principalTable: "OrderLine",
                principalColumns: new[] { "orderID", "lineID" });

            migrationBuilder.AddForeignKey(
                name: "FK__OrderHead__custo__656C112C",
                table: "OrderHeader",
                column: "customerID",
                principalTable: "Customer",
                principalColumn: "customerID");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderLine__order__68487DD7",
                table: "OrderLine",
                column: "orderID",
                principalTable: "OrderHeader",
                principalColumn: "orderID");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderLine__produ__693CA210",
                table: "OrderLine",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID");
        }
    }
}
