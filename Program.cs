using LegacyOrderService.Data;
using LegacyOrderService.HostedServices;
using LegacyOrderService.Repositories;
using LegacyOrderService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

var host = Host.CreateDefaultBuilder(args)
// NLog configuration
.ConfigureLogging(logging =>
{
    logging.ClearProviders(); // Remove default providers
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
})
.UseNLog() // Use NLog for logging
.ConfigureAppConfiguration((ctx, builder) =>
{
    builder.AddJsonFile("appsettings.json", optional: true);
})
.ConfigureServices((ctx, services) =>
{
    var configuration = ctx.Configuration;
    var dbPath = configuration.GetValue<string>("Database:Path") ?? "orders.db";
    var connectionString = $"Data Source={dbPath}";

    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString));

    // Repos & services
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IOrderService, OrderService>();

    // Hosted interactive console worker
    services.AddHostedService<InteractiveOrderWorker>();
})
.Build();

await host.RunAsync();

