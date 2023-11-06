
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Library.DbModel.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
