
using MongoDB.Bson.Serialization.Attributes;

namespace TestApp.DbModel.Models
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
