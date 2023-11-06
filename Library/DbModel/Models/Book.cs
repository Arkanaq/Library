using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Library.DbModel.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;

    }
}
