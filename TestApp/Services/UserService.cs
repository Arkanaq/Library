using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(
            IMongoClient client,
            IOptions<LibraryStoreDbSettings> libraryStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(libraryStoreDbSettings.Value.DatabaseName);
            _users = mongoDb.GetCollection<User>(libraryStoreDbSettings.Value.UserCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
                await _users.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(Guid id) =>
            await _users.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _users.InsertOneAsync(newUser);

        public async Task UpdateAsync(Guid id, User updatedUser) =>
            await _users.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(Guid id) =>
            await _users.DeleteOneAsync(x => x.Id == id);
    }

}