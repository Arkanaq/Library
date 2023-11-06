using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.DbModel.Models
{
    [PrimaryKey(nameof(UserId), nameof(BookId))]
    public class UserLibrary
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
