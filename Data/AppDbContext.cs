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
        }
    }
}
