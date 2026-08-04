using Microsoft.EntityFrameworkCore;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Domain.Entities.Finance;
using Momentum.Infrastructure.Persistence.Context;

namespace Momentum.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(Guid userId, string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.UserId == userId && c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllByUserAsync(Guid userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}