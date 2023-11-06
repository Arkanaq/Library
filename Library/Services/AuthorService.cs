
using Library.DbModel;
using Library.DbModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorService
    {
        private readonly MongoDbContext _dbContext;

        public AuthorService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Author> GetAsync() => _dbContext.Authors.AsQueryable();

        public async Task<Author?> GetAsync(Guid id) =>
            await _dbContext.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Author?> GetAsync(string fullName) =>
            await _dbContext.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.FullName == fullName);

        public async Task CreateAsync(Author newAuthor)
        {
            await _dbContext.Authors.AddAsync(newAuthor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author updatedAuthor)
        {
            _dbContext.Authors.Update(updatedAuthor);
            await _dbContext.SaveChangesAsync();

        }

        public async Task RemoveAsync(Author author)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
    }

}