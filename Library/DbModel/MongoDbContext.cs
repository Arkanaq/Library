using Library.DbModel.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Library.DbModel
{
    public class MongoDbContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserLibrary> Library { get; set; }


        public MongoDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>(builder => { builder.ToCollection("Author"); });
            modelBuilder.Entity<Book>(builder =>
            {
                builder.ToCollection("Book");
                builder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
            });
            modelBuilder.Entity<User>().ToCollection("User");
            modelBuilder.Entity<UserLibrary>().ToCollection("Library");
        }
    }
}
