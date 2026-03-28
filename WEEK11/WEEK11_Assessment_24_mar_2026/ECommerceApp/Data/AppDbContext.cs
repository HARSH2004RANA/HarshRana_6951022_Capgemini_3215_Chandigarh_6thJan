using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace ECommerceApp.Data
{
    

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingDetail)
                .WithOne(s => s.Order)
                .HasForeignKey<ShippingDetail>(s => s.OrderId);

            // Many-to-Many via OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(p => p.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
