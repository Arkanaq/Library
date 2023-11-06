using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.Xml;
using Library.DbModel;
using Library.DbModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class LibraryService
    {
        private readonly MongoDbContext _dbContext;

        public LibraryService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserLibrary>> GetAsync() =>
            await _dbContext.Library.AsQueryable().ToListAsync();

        public async Task<UserLibrary?> GetUserLibrary(Guid userId) =>
            await _dbContext.Library.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task CreateAsync(UserLibrary newLibrary)
        {
            await _dbContext.Library.AddAsync(newLibrary);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBookFromLibraryAsync(Guid bookId, Guid userId)
        {
            var library = await _dbContext.Library.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId);
            if (library == null) return;

            _dbContext.Library.Remove(library);

            await _dbContext.SaveChangesAsync();
        }

    }
}