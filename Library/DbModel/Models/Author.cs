using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Library.DbModel.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [NotMapped]
        public string FullName => $"{Name} {SurName}";

        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
        [JsonIgnore]
        public DateTime EditTime { get; set; }

        public ICollection<Book> Books { get; set; }
    }

}
