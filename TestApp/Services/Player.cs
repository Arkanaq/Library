using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.DbModel;
using TestApp.DbModel.Models;

namespace TestApp.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<Author> _players;

        public PlayerService(
            IMongoClient client,
            IOptions<GameStoreDatabaseSettings> gameStoreDbSettings
            )
        {

            var mongoDb = client.GetDatabase(gameStoreDbSettings.Value.DatabaseName);
            _players = mongoDb.GetCollection<Author>(gameStoreDbSettings.Value.PlayerCollectionName);
        }

        public async Task<List<Author>> GetAsync() =>
                await _players.Find(_ => true).ToListAsync();

        public async Task<Author?> GetAsync(Guid id) =>
            await _players.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Author newPlayer) =>
            await _players.InsertOneAsync(newPlayer);

        public async Task UpdateAsync(Guid id, Author updatedPlayer) =>
            await _players.ReplaceOneAsync(x => x.Id == id, updatedPlayer);

        public async Task RemoveAsync(Guid id) =>
            await _players.DeleteOneAsync(x => x.Id == id);
    }

}