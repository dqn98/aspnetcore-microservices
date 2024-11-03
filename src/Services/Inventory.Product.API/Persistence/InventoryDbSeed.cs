using Inventory.Product.API.Entities.Abstraction;
using Inventory.Product.API.Extensions;
using MongoDB.Driver;
using Shared.Enums.Inventory;

namespace Inventory.Product.API.Persistence;

public class InventoryDbSeed
{
    public async Task SeedDataAsync(IMongoClient mongoClient, DatabaseSettings settings)
    {
        string databaseName = settings.DatabaseName;
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        var inventoryCollection = database.GetCollection<InventoryEntry>("InventoryEntries");
        if (await inventoryCollection.EstimatedDocumentCountAsync() == 0)
        {
            await inventoryCollection.InsertManyAsync(GetPreconfiguredInventoryEntries());
        }
    }
    
    private IEnumerable<InventoryEntry> GetPreconfiguredInventoryEntries()
    {
        return new List<InventoryEntry>
        {
            new()
            {
                Quantity = 10,
                DocumentNo = Guid.NewGuid().ToString(),
                ItemNo = "Lotus",
                ExternalDocumentNo = Guid.NewGuid().ToString(),
                DocumentType = EDocumentType.Purchase
            },
            new()
            {
                Quantity = 10,
                DocumentNo = Guid.NewGuid().ToString(),
                ItemNo = "Cadillac",
                ExternalDocumentNo = Guid.NewGuid().ToString(),
                DocumentType = EDocumentType.Purchase
            }
        };
    }
}