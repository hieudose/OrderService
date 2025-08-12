using LegacyOrderService.Models;
using LegacyOrderService.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.HostedServices
{
    public class InteractiveOrderWorker : BackgroundService
    {
        private readonly IOrderService _orderService;

        public InteractiveOrderWorker(IOrderService orderService)
        {
            _orderService = orderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Welcome to Order Processor!");

                Console.Write("Enter customer name: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Enter product name: ");
                string product = Console.ReadLine() ?? "";

                double price = 0;
                try
                {
                    price = await _orderService.GetUnitPriceAsync(product);
                }
                catch (Exception ex)
                {
                    ExitWithMessage(ex.Message);
                    continue;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    ExitWithMessage("Invalid quantity.");
                    continue;
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

                await _orderService.CreateOrderAsync(order);

                ExitWithMessage("Order saved to database.");
            }

        }
        static void ExitWithMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key clear...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
