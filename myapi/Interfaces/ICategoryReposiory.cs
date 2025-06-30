using myapi.Dtos.Category;
using myapi.Models;

namespace myapi.Interfaces
{
    public interface ICategoryReposiory
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category?> DeleteAsync(int id);

    }
    
}