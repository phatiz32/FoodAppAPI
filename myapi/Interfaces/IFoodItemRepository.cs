using myapi.Dtos.FoodItem;
using myapi.Helpers;
using myapi.Models;

namespace myapi.Interfaces
{
    public interface IFoodItemRepository
    {
        Task<FoodItem> CreateAsync(CreateFoodItemDto foodItemDto);
        Task<List<ToFoodItemDto>> GetAllAsync(QueryObject query);
        Task<FoodItem?> UpdateAsync(int id, UpdateFoodItemDto foodItemDto);
        Task<FoodItem?> DeleteAsync(int id);
    }
}