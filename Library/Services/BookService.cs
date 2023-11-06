using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Library.DbModel;
using Library.DbModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService
    {
        private readonly MongoDbContext _dbContext;
        public BookService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAsync() =>
            await _dbContext.Books.ToListAsync();

        public async Task<Book?> GetAsync(Guid id) =>
            await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);

        public async Task CreateAsync(Book newBook) =>
            await _dbContext.Books.AddAsync(newBook);

        public async Task UpdateAsync(Book updatedBook)
        {
            _dbContext.Books.Update(updatedBook);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Book book)
        {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
        }
           
    }

}