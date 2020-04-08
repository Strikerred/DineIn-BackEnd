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

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CustomerInfo> CustomerInfo { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<RestaurantInfo> RestaurantInfo { get; set; }
        public virtual DbSet<TransportationType> TransportationType { get; set; }
        public virtual DbSet<ItemSelected> ItemSelected { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();
            });

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

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ItemSelected>(entity =>
            {
                entity.Property(e => e.ItemSelectedId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.ItemSelected)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.ItemId).IsRequired();
               
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
