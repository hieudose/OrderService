using LegacyOrderService.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Repositories
{
    public class OrderRepository(string connectionString) : IOrderRepository
    {
        public async Task AddAsync(Order order)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
            VALUES ($customerName, $productName, $quantity, $price)";

            command.Parameters.AddWithValue("$customerName", order.CustomerName);
            command.Parameters.AddWithValue("$productName", order.ProductName);
            command.Parameters.AddWithValue("$quantity", order.Quantity);
            command.Parameters.AddWithValue("$price", order.Price);

            await command.ExecuteNonQueryAsync();
        }
    }
}
