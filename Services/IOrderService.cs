using LegacyOrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task<double> GetUnitPriceAsync(string productName);
    }
}
