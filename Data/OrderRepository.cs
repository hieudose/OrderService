using System;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderRepository(string connectionString)
    {
        public void Save(Order order)
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

            command.ExecuteNonQuery();
        }
    }
}
