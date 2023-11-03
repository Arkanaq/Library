using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _items;

        public ItemService(
            IMongoClient client,
            IOptions<LibraryStoreDbSettings> gameStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(gameStoreDbSettings.Value.DatabaseName);
            _items = mongoDb.GetCollection<Item>(gameStoreDbSettings.Value.ItemCollectionName);
        }

        public async Task<List<Item>> GetAsync() =>
                await _items.Find(_ => true).ToListAsync();

        public async Task<Item?> GetAsync(Guid id) =>
            await _items.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Item newBook) =>
            await _items.InsertOneAsync(newBook);

        public async Task UpdateAsync(Guid id, Item updatedBook) =>
            await _items.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(Guid id) =>
            await _items.DeleteOneAsync(x => x.Id == id);
    }

}