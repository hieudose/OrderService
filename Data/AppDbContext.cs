using LegacyOrderService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.CustomerName).IsRequired();
                b.Property(o => o.ProductName).IsRequired();
                b.Property(o => o.Price).IsRequired();
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.ProductName).IsRequired();
                b.Property(o => o.Price).IsRequired();
            });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "Widget", Price = 12.99 },
                new Product { Id = 2, ProductName = "Gadget", Price = 15.49 },
                new Product { Id = 3, ProductName = "Doohickey", Price = 8.75 }
            );
        }
    }
}
