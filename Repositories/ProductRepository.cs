using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IReadOnlyDictionary<string, double> _prices = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public Task<double> GetUnitPriceAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("productName required", nameof(productName));

            // Simulate an expensive lookup
            Thread.Sleep(500);

            if (_prices.TryGetValue(productName, out var price))
                return Task.FromResult(price);

            throw new KeyNotFoundException($"Product '{productName}' not found.");
        }
    }
}
