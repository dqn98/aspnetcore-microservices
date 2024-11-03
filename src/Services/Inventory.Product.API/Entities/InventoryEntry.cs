using Inventory.Product.API.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums.Inventory;

namespace Inventory.Product.API.Entities.Abstraction;

[BSonCollection("InventoryEntries")]
public class InventoryEntry : MongoEntity
{
    public InventoryEntry()
    {
        DocumentType = EDocumentType.Purchase;
        DocumentNo = Guid.NewGuid().ToString();
        ExternalDocumentNo = Guid.NewGuid().ToString();
    }
    public InventoryEntry(string id) => (Id) = id;
    
    [BsonElement("documentType")]
    public EDocumentType DocumentType { get; set; }
    
    [BsonElement("documentNo")]
    public string DocumentNo { get; set; }
    
    [BsonElement("itemNo")]
    public string? ItemNo { get; set; }
    
    [BsonElement("quantity")]
    public int Quantity { get; set; }
    
    [BsonElement("externalDocumentNo")]
    public string ExternalDocumentNo { get; set; }
}