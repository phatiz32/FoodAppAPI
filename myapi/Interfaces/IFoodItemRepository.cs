using myapi.Dtos.FoodItem;
using myapi.Models;

namespace myapi.Interfaces
{
    public interface IFoodItemRepository
    {
        Task<FoodItem> CreateAsync(CreateFoodItemDto foodItemDto);
    }
}