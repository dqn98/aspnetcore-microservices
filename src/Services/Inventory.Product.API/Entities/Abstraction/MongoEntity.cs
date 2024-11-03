using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Product.API.Entities.Abstraction;

public class MongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public virtual string? Id { get; protected init; }
    
    [BsonElement("creationDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [BsonElement("lastModifiedDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? LastModifiedDate { get; set; } = DateTime.UtcNow;
}