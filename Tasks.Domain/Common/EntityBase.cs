using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Tasks.Domain.Common
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool isActive { get; set; } = true;
    }
}
