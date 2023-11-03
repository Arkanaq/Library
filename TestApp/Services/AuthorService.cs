using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class AuthorService
    {
        private readonly IMongoCollection<Author> _author;

        public AuthorService(
            IMongoClient client,
            IOptions<LibraryStoreDbSettings> libraryStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(libraryStoreDbSettings.Value.DatabaseName);
            _author = mongoDb.GetCollection<Author>(libraryStoreDbSettings.Value.AuthorCollectionName);
        }

        public async Task<List<Author>> GetAsync() =>
                await _author.Find(_ => true).ToListAsync();

        public async Task<Author?> GetAsync(Guid id) =>
            await _author.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Author newAuthor) =>
            await _author.InsertOneAsync(newAuthor);

        public async Task UpdateAsync(Guid id, Author updatedAuthor) =>
            await _author.ReplaceOneAsync(x => x.Id == id, updatedAuthor);

        public async Task RemoveAsync(Guid id) =>
            await _author.DeleteOneAsync(x => x.Id == id);
    }

}