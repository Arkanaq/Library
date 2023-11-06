using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Library.DbModel;
using Library.DbModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class UserService
    {
        private readonly MongoDbContext _dbContext;

        public UserService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAsync() =>
                await _dbContext.Users.ToListAsync();

        public async Task<User?> GetAsync(Guid id) =>
            await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(User newUser)
        {
           await _dbContext.Users.AddAsync(newUser);
           await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User updatedUser)
        {
            _dbContext.Users.Update(updatedUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }

}