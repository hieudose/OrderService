using LegacyOrderService.Models;
using LegacyOrderService.Services;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceScopeFactory _scopeFactory;

        public InteractiveOrderWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                    Console.WriteLine("Welcome to Order Processor!");

                    Console.Write("Enter customer name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Enter product name: ");
                    string product = Console.ReadLine() ?? "";

                    double price = 0;
                    try
                    {
                        price = await orderService.GetUnitPriceAsync(product);
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

                    await orderService.CreateOrderAsync(order);

                    ExitWithMessage("Order saved to database.");
                }
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
