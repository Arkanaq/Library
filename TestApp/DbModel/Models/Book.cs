using MongoDB.Bson.Serialization.Attributes;

namespace TestApp.DbModel.Models
{
    public class Book
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Pages { get; set; }
        public string Content { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }

    }
}
