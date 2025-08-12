using LegacyOrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(string productName);
    }
}
