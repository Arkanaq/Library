using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TestApp.DbModel.Models
{
    public class Author
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }

    }

}
