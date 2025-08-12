using LegacyOrderService.Data;
using LegacyOrderService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Repositories
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<Product> GetProductAsync(string productName)
        {
            return await dbContext.Products.FirstOrDefaultAsync(n => n.ProductName == productName);
        }
    }
}
