using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodOrderApp.Models
{
    public partial class sqlContext : DbContext
    {
        public sqlContext()
        {
        }

        public sqlContext(DbContextOptions<sqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerInfo> CustomerInfo { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<RestaurantInfo> RestaurantInfo { get; set; }
        public virtual DbSet<TransportationType> TransportationType { get; set; }
        public virtual DbSet<SelectedItem> SelectedItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.UserRole).IsRequired();

                entity.Property(e => e.UsersEmail).IsRequired();
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.Property(e => e.FoodCategoryId).ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryName).IsRequired();
            });

            modelBuilder.Entity<MenuItems>(entity =>
            {
                entity.HasKey(e => e.MenuItemId);

                entity.Property(e => e.MenuItemId).ValueGeneratedOnAdd();

                entity.Property(e => e.DishName).IsRequired();

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MenuSection).IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("NUMERIC");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SelectedItem>(entity =>
            {
                entity.Property(e => e.SelectedItemId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.SelectedItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MenuItems)
                    .WithMany(p => p.SelectedItem)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.Quantity).IsRequired();
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.PaymentTypeId).ValueGeneratedOnAdd();

                entity.Property(e => e.PaymentName).IsRequired();
            });

            modelBuilder.Entity<RestaurantInfo>(entity =>
            {
                entity.HasKey(e => e.RestaurantId);

                entity.Property(e => e.RestaurantId).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.RestaurantName).IsRequired();

                entity.HasOne(d => d.FoodCategory)
                    .WithMany(p => p.RestaurantInfo)
                    .HasForeignKey(d => d.FoodCategoryId);
            });

            modelBuilder.Entity<TransportationType>(entity =>
            {
                entity.HasKey(e => e.TransportationId);

                entity.Property(e => e.TransportationId).ValueGeneratedOnAdd();

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.TransportationName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
