using MongoDB.Bson.Serialization.Attributes;

namespace TestApp.DbModel.Models
{
    public class Author
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }

    }

}
