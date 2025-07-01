using Microsoft.EntityFrameworkCore.Diagnostics;
using myapi.Data;
using myapi.Dtos.FoodItem;
using myapi.Interfaces;
using myapi.Mappers;
using myapi.Models;

namespace myapi.Repository
{
    public class FoodItemRespository : IFoodItemRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _env;

        public FoodItemRespository(ApplicationDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<FoodItem> CreateAsync(CreateFoodItemDto dto)
        {
             var food = dto.toCreateFoodDto(); // dùng mapper

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                food.ImageUrl = fileName;
            }

            await _context.FoodItems.AddAsync(food);
            await _context.SaveChangesAsync();

            return food;
        }
    }
}