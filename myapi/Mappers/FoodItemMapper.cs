using myapi.Dtos.Category;
using myapi.Dtos.FoodItem;
using myapi.Models;

namespace myapi.Mappers
{
    public static class FoodItemMapper
    {
        public static FoodItem toCreateFoodDto(this CreateFoodItemDto dto)
        {
            return new FoodItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsAvailable=dto.IsAvailable,
                CategoryId = dto.CategoryId,
                
            };
        }
    }
}