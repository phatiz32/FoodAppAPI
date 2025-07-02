using Humanizer;
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
                IsAvailable = dto.IsAvailable,
                CategoryId = dto.CategoryId,

            };
        }
        public static ToFoodItemDto toFoodItemDto(this FoodItem model)
        {
            Console.WriteLine($"DEBUG: {model.Name} - Category = {model.Category?.Name}");
            return new ToFoodItemDto
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsAvailable = model.IsAvailable,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                CategoryName = model.Category?.Name ?? ""
            };
        }
        
    }
}