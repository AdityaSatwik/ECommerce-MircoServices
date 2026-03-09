using ECommerce.OrderService.Services;
using ECommerce.OrderService.Services.Interfaces;

/// <summary>
/// Entry point for the Order Management microservice.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add HTTP Client for Inventory Service
builder.Services.AddHttpClient<IInventoryService, InventoryService>(client =>
{
    var inventoryUrl = builder.Configuration["InventoryServiceUrl"] ?? "http://api-gateway:8080/api/inventory";
    client.BaseAddress = new Uri(inventoryUrl);
});

var app = builder.Build();

app.MapControllers();

app.Run();
