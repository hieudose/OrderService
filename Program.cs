using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "orders.db");
            string connectionString = $"Data Source={dbPath}";

            var productRepo = new ProductRepository();
            var orderRepo = new OrderRepository(connectionString);

            Console.WriteLine("Welcome to Order Processor!");

            Console.Write("Enter customer name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter product name: ");
            string product = Console.ReadLine() ?? "";

            double price = 0;
            try
            {
                price = productRepo.GetPrice(product);
            }
            catch (Exception ex)
            {
                ExitWithMessage(ex.Message);
                return;
            }

            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                ExitWithMessage("Invalid quantity.");
                return;
            }

            var order = new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = qty,
                Price = price
            };

            double total = order.Quantity * order.Price;

            Console.WriteLine("Processing order...");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"Product: {order.ProductName}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Total: ${total:F2}");

            orderRepo.Save(order);

            ExitWithMessage("Order saved to database.");
        }
        static void ExitWithMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
