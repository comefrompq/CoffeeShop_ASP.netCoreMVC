using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeShop.Data
{
    public class CoffeeShopDbContext:DbContext
    {
        public CoffeeShopDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(sc => new { sc.OrderId, sc.ProductId });
        }
    }
}
