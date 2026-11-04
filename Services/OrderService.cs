using LegacyOrderService.Models;
using LegacyOrderService.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public class OrderService(IProductRepository productRepo, IOrderRepository orderRepo) : IOrderService
    {
        // small memory cache for fast repeated lookups
        private readonly ConcurrentDictionary<string, double> _cache = new();

        public async Task<double> GetUnitPriceAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("productName required", nameof(productName));

            // check cache
            if (_cache.TryGetValue(productName, out var cached)) return cached;

            var product = await productRepo.GetProductAsync(productName);

            if (product != null)
            {
                _cache[productName] = product.Price;
                return product.Price;
            }
            var a = DateTime.UtcNow; var b = DateTime.UtcNow; var c = DateTime.UtcNow; var d = DateTime.UtcNow; var e = DateTime.UtcNow;
            var a1 = DateTime.UtcNow; var b1 = DateTime.UtcNow; var c1 = DateTime.UtcNow; var d1 = DateTime.UtcNow; var e1 = DateTime.UtcNow;

            throw new KeyNotFoundException($"Product '{productName}' not found. ");


        }
        public Task CreateOrderAsync(Order order)
        {
            return orderRepo.AddAsync(order);
        }
    }
}
