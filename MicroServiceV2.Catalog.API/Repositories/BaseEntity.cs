using MongoDB.Bson.Serialization.Attributes;

namespace MicroServiceV2.Catalog.API.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
