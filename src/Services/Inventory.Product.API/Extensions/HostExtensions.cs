using Inventory.Product.API.Persistence;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;
        var setting = services.GetService<DatabaseSettings>();
        if (setting == null || string.IsNullOrEmpty(setting.ConnectionString))
            throw new ArgumentNullException("Database Settings is not configured.");
        
        var mongoClient = services.GetRequiredService<IMongoClient>();
        new InventoryDbSeed()
            .SeedDataAsync(mongoClient, setting)
            .Wait();
        return host;
    }
}