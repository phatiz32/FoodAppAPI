using System.ComponentModel.DataAnnotations;

namespace myapi.Dtos.FoodItem
{
    public class UpdateFoodItemDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public bool? IsAvailable { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile? Image{ get; set; }//upload anh
    }
}