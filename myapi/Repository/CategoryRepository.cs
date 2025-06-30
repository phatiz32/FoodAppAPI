using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Repository
{
    public class CategoryRepository : ICategoryReposiory
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categorys = await _context.Categories.ToListAsync();
            return categorys;
        }


    }
}