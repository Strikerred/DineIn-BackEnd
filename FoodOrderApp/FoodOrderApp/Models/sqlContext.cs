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
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<RestaurantInfo> RestaurantInfo { get; set; }
        public virtual DbSet<TransportationType> TransportationType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source= .\\wwwroot\\sql.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.HasIndex(e => e.CustomerId)
                    .IsUnique();

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("NUMERIC");
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.HasIndex(e => e.FoodCategoryId)
                    .IsUnique();

                entity.Property(e => e.FoodCategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).IsRequired();
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasIndex(e => e.MenuItemId)
                    .IsUnique();

                entity.Property(e => e.MenuItemId).ValueGeneratedNever();

                entity.Property(e => e.DishName).IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("NUMERIC");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderId)
                    .IsUnique();

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.OrderTotal)
                    .IsRequired()
                    .HasColumnType("NUMERIC");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.HasIndex(e => e.PaymentTypeId)
                    .IsUnique();

                entity.Property(e => e.PaymentTypeId).ValueGeneratedNever();

                entity.Property(e => e.PaymentName).IsRequired();
            });

            modelBuilder.Entity<RestaurantInfo>(entity =>
            {
                entity.HasKey(e => e.RestaurantId);

                entity.HasIndex(e => e.RestaurantId)
                    .IsUnique();

                entity.Property(e => e.RestaurantId).ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.RestaurantName).IsRequired();
            });

            modelBuilder.Entity<TransportationType>(entity =>
            {
                entity.HasKey(e => e.TransportationId);

                entity.HasIndex(e => e.TransportationId)
                    .IsUnique();

                entity.Property(e => e.TransportationId).ValueGeneratedNever();

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.TransportationName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
