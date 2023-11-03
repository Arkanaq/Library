using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(
            IMongoClient client,
            IOptions<LibraryStoreDbSettings> libraryStoreSettings
            )
        {

            var mongoDb = client.GetDatabase(libraryStoreSettings.Value.DatabaseName);
            _books = mongoDb.GetCollection<Book>(libraryStoreSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
                await _books.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(Guid id) =>
            await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) =>
            await _books.InsertOneAsync(newBook);

        public async Task UpdateAsync(Guid id, Book updatedBook) =>
            await _books.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(Guid id) =>
            await _books.DeleteOneAsync(x => x.Id == id);
    }

}