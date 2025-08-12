using LegacyOrderService.Models;
using LegacyOrderService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public class OrderService(IProductRepository productRepo, IOrderRepository orderRepo) : IOrderService
    {
        public Task<double> GetUnitPriceAsync(string productName)
        {
            return productRepo.GetUnitPriceAsync(productName);
        }
        public Task CreateOrderAsync(Order order)
        {
            return orderRepo.AddAsync(order);
        }
    }
}
