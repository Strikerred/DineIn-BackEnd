using System;
using System.Collections.Generic;
using System.Text;
using FoodOrderApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
            public DbSet<RestaurantInfo> RestaurantInfo { get; set; }
            public DbSet<MenuItems> MenuItems { get; set; }
            public DbSet<Users> Users { get; set; }
            public DbSet<Orders> Orders { get; set; }
        
    }
}
    