using LegacyOrderService.Data;
using LegacyOrderService.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : IOrderRepository
    {
        public async Task AddAsync(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
