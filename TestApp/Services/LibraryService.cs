using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.Xml;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class LibraryService
    {
        private readonly IMongoCollection<UserLibrary> _libraries;

        public LibraryService(
            IMongoClient client,
            IOptions<LibraryStoreDbSettings> libraryStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(libraryStoreDbSettings.Value.DatabaseName);
            _libraries = mongoDb.GetCollection<UserLibrary>(libraryStoreDbSettings.Value.LibraryCollectionName);
        }

        public async Task<List<UserLibrary>> GetAsync() =>
                await _libraries.Find(_ => true).ToListAsync();

        public async Task<UserLibrary?> GetUserLibrary(Guid userId) =>
            await _libraries.Find(x => x.UserId == userId).FirstOrDefaultAsync();

        public async Task CreateAsync(UserLibrary newLibrary) =>
            await _libraries.InsertOneAsync(newLibrary);

        public async Task RemoveBookFromLibraryAsync(Guid bookId, Guid userId) =>
            await _libraries.DeleteOneAsync(x => x.UserId == userId && x.BookId == bookId);
    }

}