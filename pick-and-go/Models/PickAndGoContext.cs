using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace pick_and_go.Models
{
    public partial class PickAndGoContext : DbContext
    {
        public PickAndGoContext()
        {
        }

        public PickAndGoContext(DbContextOptions<PickAndGoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DietaryType> DietaryTypes { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<LineIngredient> LineIngredients { get; set; } = null!;
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; } = null!;
        public virtual DbSet<OrderLine> OrderLines { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= KCHONG1902\\SQLEXPRESS_KC3;Database=PickAndGo;Trusted_Connection=True; TrustServerCertificate=True ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("categoryID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.AdminUser)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("adminUser")
                    .IsFixedLength();

                entity.Property(e => e.DateLastOrdered)
                    .HasColumnType("date")
                    .HasColumnName("dateLastOrdered");

                entity.Property(e => e.DateSignedUp)
                    .HasColumnType("date")
                    .HasColumnName("dateSignedUp");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("emailAddress");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.HasMany(d => d.Dietaries)
                    .WithMany(p => p.Customers)
                    .UsingEntity<Dictionary<string, object>>(
                        "CustomerDietaryType",
                        l => l.HasOne<DietaryType>().WithMany().HasForeignKey("DietaryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Customer___dieta__787EE5A0"),
                        r => r.HasOne<Customer>().WithMany().HasForeignKey("CustomerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Customer___custo__778AC167"),
                        j =>
                        {
                            j.HasKey("CustomerId", "DietaryId");

                            j.ToTable("Customer_DietaryType");

                            j.IndexerProperty<int>("CustomerId").HasColumnName("customerID");

                            j.IndexerProperty<string>("DietaryId").HasMaxLength(2).IsUnicode(false).HasColumnName("dietaryID");
                        });
            });

            modelBuilder.Entity<DietaryType>(entity =>
            {
                entity.HasKey(e => e.DietaryId)
                    .HasName("PK__DietaryT__9B5E3E4CF087F04B");

                entity.ToTable("DietaryType");

                entity.Property(e => e.DietaryId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("dietaryID");

                entity.Property(e => e.DietaryImage)
                    .HasColumnType("image")
                    .HasColumnName("dietaryImage");

                entity.Property(e => e.DietaryName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("dietaryName");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.OrderId, e.LineId });

                entity.ToTable("Favorite");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.LineId).HasColumnName("lineID");

                entity.Property(e => e.FavoriteName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("favoriteName");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__custom__70DDC3D8");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__orderI__71D1E811");

                entity.HasOne(d => d.OrderLine)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => new { d.OrderId, d.LineId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__72C60C4A");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientID");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("categoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.InStock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("inStock")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__categ__628FA481");

                entity.HasMany(d => d.Dietaries)
                    .WithMany(p => p.Ingredients)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientDietaryType",
                        l => l.HasOne<DietaryType>().WithMany().HasForeignKey("DietaryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__dieta__7C4F7684"),
                        r => r.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__ingre__7B5B524B"),
                        j =>
                        {
                            j.HasKey("IngredientId", "DietaryId");

                            j.ToTable("Ingredient_DietaryType");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("ingredientID");

                            j.IndexerProperty<string>("DietaryId").HasMaxLength(2).IsUnicode(false).HasColumnName("dietaryID");
                        });
            });

            modelBuilder.Entity<LineIngredient>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.LineId, e.IngredientId });

                entity.ToTable("LineIngredient");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.LineId).HasColumnName("lineID");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngre__ingre__6C190EBB");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngre__order__6D0D32F4");

                entity.HasOne(d => d.OrderLine)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => new { d.OrderId, d.LineId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngredient__6E01572D");
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderHea__0809337DA99DA31A");

                entity.ToTable("OrderHeader");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("orderStatus")
                    .IsFixedLength();

                entity.Property(e => e.OrderValue)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("orderValue");

                entity.Property(e => e.PickupTime).HasColumnName("pickupTime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHead__custo__656C112C");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.LineId });

                entity.ToTable("OrderLine");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.LineId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("lineID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__order__68487DD7");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__produ__693CA210");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("productID");

                entity.Property(e => e.BasePrice)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("basePrice");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
