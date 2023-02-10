﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PickAndGo.Models;

#nullable disable

namespace PickAndGo.Migrations
{
    [DbContext(typeof(PickAndGoContext))]
    [Migration("20230210194809_0210-2")]
    partial class _02102
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("CustomerDietaryType", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customerID");

                    b.Property<string>("DietaryId")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("dietaryID");

                    b.HasKey("CustomerId", "DietaryId");

                    b.HasIndex(new[] { "DietaryId" }, "IX_Customer_DietaryType_dietaryID");

                    b.ToTable("Customer_DietaryType", (string)null);
                });

            modelBuilder.Entity("IngredientDietaryType", b =>
                {
                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnName("ingredientID");

                    b.Property<string>("DietaryId")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("dietaryID");

                    b.HasKey("IngredientId", "DietaryId");

                    b.HasIndex(new[] { "DietaryId" }, "IX_Ingredient_DietaryType_dietaryID");

                    b.ToTable("Ingredient_DietaryType", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedName" }, "RoleNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedName] IS NOT NULL)");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleId" }, "IX_AspNetRoleClaims_RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedEmail" }, "EmailIndex");

                    b.HasIndex(new[] { "NormalizedUserName" }, "UserNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PickAndGo.Models.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("categoryID");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("customerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("AdminUser")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("adminUser")
                        .IsFixedLength();

                    b.Property<DateTime?>("DateLastOrdered")
                        .HasColumnType("date")
                        .HasColumnName("dateLastOrdered");

                    b.Property<DateTime?>("DateSignedUp")
                        .HasColumnType("date")
                        .HasColumnName("dateSignedUp");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("emailAddress");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("lastName");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phoneNumber");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.DietaryType", b =>
                {
                    b.Property<string>("DietaryId")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("dietaryID");

                    b.Property<byte[]>("DietaryImage")
                        .HasColumnType("image")
                        .HasColumnName("dietaryImage");

                    b.Property<string>("DietaryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("dietaryName");

                    b.HasKey("DietaryId")
                        .HasName("PK__DietaryT__9B5E3E4CF087F04B");

                    b.ToTable("DietaryType", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.Favorite", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customerID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.Property<int>("LineId")
                        .HasColumnType("int")
                        .HasColumnName("lineID");

                    b.Property<string>("FavoriteName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("favoriteName");

                    b.HasKey("CustomerId", "OrderId", "LineId");

                    b.HasIndex(new[] { "OrderId", "LineId" }, "IX_Favorite_orderID_lineID");

                    b.ToTable("Favorite", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ingredientID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"), 1L, 1);

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("categoryID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("description");

                    b.Property<string>("InStock")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("inStock")
                        .IsFixedLength();

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("price");

                    b.HasKey("IngredientId");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Ingredient_categoryID");

                    b.ToTable("Ingredient", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.LineIngredient", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.Property<int>("LineId")
                        .HasColumnType("int")
                        .HasColumnName("lineID");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnName("ingredientID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderId", "LineId", "IngredientId");

                    b.HasIndex(new[] { "IngredientId" }, "IX_LineIngredient_ingredientID");

                    b.ToTable("LineIngredient", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.OrderHeader", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customerID");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("orderDate");

                    b.Property<string>("OrderStatus")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("orderStatus")
                        .IsFixedLength();

                    b.Property<decimal?>("OrderValue")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("orderValue");

                    b.Property<DateTime?>("PickupTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("pickupTime");

                    b.HasKey("OrderId")
                        .HasName("PK__OrderHea__0809337DA99DA31A");

                    b.HasIndex(new[] { "CustomerId" }, "IX_OrderHeader_customerID");

                    b.ToTable("OrderHeader", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.Property<int>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("lineID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineId"), 1L, 1);

                    b.Property<string>("LineStatus")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("lineStatus")
                        .HasDefaultValueSql("('O')")
                        .IsFixedLength();

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("OrderId", "LineId");

                    b.HasIndex(new[] { "ProductId" }, "IX_OrderLine_productID");

                    b.ToTable("OrderLine", (string)null);
                });

            modelBuilder.Entity("PickAndGo.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productID");

                    b.Property<decimal?>("BasePrice")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("basePrice");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("description");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image")
                        .HasColumnName("image");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("AspNetUserRole", b =>
                {
                    b.HasOne("PickAndGo.Models.AspNetRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PickAndGo.Models.AspNetUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CustomerDietaryType", b =>
                {
                    b.HasOne("PickAndGo.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Customer___custo__778AC167");

                    b.HasOne("PickAndGo.Models.DietaryType", null)
                        .WithMany()
                        .HasForeignKey("DietaryId")
                        .IsRequired()
                        .HasConstraintName("FK__Customer___dieta__787EE5A0");
                });

            modelBuilder.Entity("IngredientDietaryType", b =>
                {
                    b.HasOne("PickAndGo.Models.DietaryType", null)
                        .WithMany()
                        .HasForeignKey("DietaryId")
                        .IsRequired()
                        .HasConstraintName("FK__Ingredien__dieta__7C4F7684");

                    b.HasOne("PickAndGo.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .IsRequired()
                        .HasConstraintName("FK__Ingredien__ingre__7B5B524B");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetRoleClaim", b =>
                {
                    b.HasOne("PickAndGo.Models.AspNetRole", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserClaim", b =>
                {
                    b.HasOne("PickAndGo.Models.AspNetUser", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserLogin", b =>
                {
                    b.HasOne("PickAndGo.Models.AspNetUser", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUserToken", b =>
                {
                    b.HasOne("PickAndGo.Models.AspNetUser", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PickAndGo.Models.Favorite", b =>
                {
                    b.HasOne("PickAndGo.Models.Customer", "Customer")
                        .WithMany("Favorites")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Favorite__custom__70DDC3D8");

                    b.HasOne("PickAndGo.Models.OrderHeader", "Order")
                        .WithMany("Favorites")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__Favorite__orderI__71D1E811");

                    b.HasOne("PickAndGo.Models.OrderLine", "OrderLine")
                        .WithMany("Favorites")
                        .HasForeignKey("OrderId", "LineId")
                        .IsRequired()
                        .HasConstraintName("FK__Favorite__72C60C4A");

                    b.Navigation("Customer");

                    b.Navigation("Order");

                    b.Navigation("OrderLine");
                });

            modelBuilder.Entity("PickAndGo.Models.Ingredient", b =>
                {
                    b.HasOne("PickAndGo.Models.Category", "Category")
                        .WithMany("Ingredients")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__Ingredien__categ__628FA481");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PickAndGo.Models.LineIngredient", b =>
                {
                    b.HasOne("PickAndGo.Models.Ingredient", "Ingredient")
                        .WithMany("LineIngredients")
                        .HasForeignKey("IngredientId")
                        .IsRequired()
                        .HasConstraintName("FK__LineIngre__ingre__6C190EBB");

                    b.HasOne("PickAndGo.Models.OrderHeader", "Order")
                        .WithMany("LineIngredients")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__LineIngre__order__6D0D32F4");

                    b.HasOne("PickAndGo.Models.OrderLine", "OrderLine")
                        .WithMany("LineIngredients")
                        .HasForeignKey("OrderId", "LineId")
                        .IsRequired()
                        .HasConstraintName("FK__LineIngredient__6E01572D");

                    b.Navigation("Ingredient");

                    b.Navigation("Order");

                    b.Navigation("OrderLine");
                });

            modelBuilder.Entity("PickAndGo.Models.OrderHeader", b =>
                {
                    b.HasOne("PickAndGo.Models.Customer", "Customer")
                        .WithMany("OrderHeaders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderHead__custo__656C112C");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PickAndGo.Models.OrderLine", b =>
                {
                    b.HasOne("PickAndGo.Models.OrderHeader", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderLine__order__68487DD7");

                    b.HasOne("PickAndGo.Models.Product", "Product")
                        .WithMany("OrderLines")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderLine__produ__693CA210");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetRole", b =>
                {
                    b.Navigation("AspNetRoleClaims");
                });

            modelBuilder.Entity("PickAndGo.Models.AspNetUser", b =>
                {
                    b.Navigation("AspNetUserClaims");

                    b.Navigation("AspNetUserLogins");

                    b.Navigation("AspNetUserTokens");
                });

            modelBuilder.Entity("PickAndGo.Models.Category", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("PickAndGo.Models.Customer", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("OrderHeaders");
                });

            modelBuilder.Entity("PickAndGo.Models.Ingredient", b =>
                {
                    b.Navigation("LineIngredients");
                });

            modelBuilder.Entity("PickAndGo.Models.OrderHeader", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("LineIngredients");

                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("PickAndGo.Models.OrderLine", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("LineIngredients");
                });

            modelBuilder.Entity("PickAndGo.Models.Product", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
