using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contracts.Domains
{
    public abstract class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public virtual string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("lastModifiedDate")]
        public DateTime? LastModifiedDate { get; set; }
    }
}
