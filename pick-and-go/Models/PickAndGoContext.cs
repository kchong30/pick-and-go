using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PickAndGo.Models
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
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=BELLE;Database=PickAndGo;Trusted_Connection=True; TrustServerCertificate=True ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                        l => l.HasOne<DietaryType>().WithMany().HasForeignKey("DietaryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Customer___dieta__34B3CB38"),
                        r => r.HasOne<Customer>().WithMany().HasForeignKey("CustomerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Customer___custo__33BFA6FF"),
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
                    .HasName("PK__DietaryT__9B5E3E4C7A2F96F0");
                entity.ToTable("DietaryType");

                entity.Property(e => e.DietaryId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("dietaryID");

                entity.Property(e => e.DietaryImage)
                    .HasMaxLength(255)
                    .IsUnicode(false)
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
                    .HasConstraintName("FK__Favorite__custom__2D12A970");
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__orderI__2E06CDA9");
                entity.HasOne(d => d.OrderLine)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => new { d.OrderId, d.LineId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__2EFAF1E2");
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
                    .HasConstraintName("FK__Ingredien__categ__1EC48A19");
                entity.HasMany(d => d.Dietaries)
                    .WithMany(p => p.Ingredients)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientDietaryType",
                        l => l.HasOne<DietaryType>().WithMany().HasForeignKey("DietaryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__dieta__38845C1C"),
                        r => r.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__ingre__379037E3"),
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

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngre__ingre__284DF453");
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngre__order__2942188C");
                entity.HasOne(d => d.OrderLine)
                    .WithMany(p => p.LineIngredients)
                    .HasForeignKey(d => new { d.OrderId, d.LineId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineIngredient__2A363CC5");
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderHea__0809337D1D31531E");
                entity.ToTable("OrderHeader");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("currency");

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

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("paymentID");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("paymentType");

                entity.Property(e => e.PickupTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pickupTime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderHead__custo__21A0F6C4");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.LineId });

                entity.ToTable("OrderLine");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.LineId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("lineID");

                entity.Property(e => e.LineStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("lineStatus")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__order__247D636F");
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__produ__257187A8");
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
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
