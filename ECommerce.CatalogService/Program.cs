using Microsoft.EntityFrameworkCore;
using ECommerce.CatalogService.Data;
using ECommerce.CatalogService.Repositories;
using System.Reflection;

/// <summary>
/// Entry point for the Product Catalog microservice.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Advanced Data Access
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// CQRS Pattern via MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create DB and tables on startup for POC
using (var scope = app.Services.CreateScope())
{
    var customDbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    
    // Simple retry logic for SQL Server startup
    for (int i = 0; i < 10; i++)
    {
        try
        {
            customDbContext.Database.EnsureCreated();
            
            // Create legacy table if not exists for the POC
            var createLegacyTableSql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'InventoryItems')
                BEGIN
                    CREATE TABLE InventoryItems (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        ProductId INT NOT NULL,
                        Quantity INT NOT NULL
                    );
                    INSERT INTO InventoryItems (ProductId, Quantity) VALUES (1, 100), (2, 50), (3, 75);
                END";
            customDbContext.Database.ExecuteSqlRaw(createLegacyTableSql);

            // Seed data if empty
            if (!customDbContext.Products.Any())
            {
                Console.WriteLine("Seeding initial products...");
                customDbContext.Products.AddRange(new List<ECommerce.CatalogService.Entities.Product>
                {
                    new ECommerce.CatalogService.Entities.Product { Name = "Gaming Laptop", Price = 1200.00m },
                    new ECommerce.CatalogService.Entities.Product { Name = "Wireless Mouse", Price = 25.50m },
                    new ECommerce.CatalogService.Entities.Product { Name = "Mechanical Keyboard", Price = 80.00m }
                });
                customDbContext.SaveChanges();
            }
            
            var count = customDbContext.Products.Count();
            Console.WriteLine($"Database ready. Product count: {count}");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed, retrying... ({ex.Message})");
            Thread.Sleep(5000);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

