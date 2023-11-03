using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class InventoryService
    {
        private readonly IMongoCollection<Book> _inventory;

        public InventoryService(
            IMongoClient client,
            IOptions<GameStoreDatabaseSettings> gameStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(gameStoreDbSettings.Value.DatabaseName);
            _inventory = mongoDb.GetCollection<Book>(gameStoreDbSettings.Value.InventoryCollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
                await _inventory.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(Guid id) =>
            await _inventory.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newInventory) =>
            await _inventory.InsertOneAsync(newInventory);

        public async Task UpdateAsync(Guid id, Book updatedInventory) =>
            await _inventory.ReplaceOneAsync(x => x.Id == id, updatedInventory);

        public async Task RemoveAsync(Guid id) =>
            await _inventory.DeleteOneAsync(x => x.Id == id);
    }

}