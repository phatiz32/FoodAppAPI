using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using myapi.Data;
using myapi.Dtos.FoodItem;
using myapi.Helpers;
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

        public async Task<List<FoodItem>> GetAllAsync(QueryObject query)
        {
            return await _context.FoodItems.Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageNumber).ToListAsync();
        }

        public async Task<FoodItem> UpdateAsync(int id, UpdateFoodItemDto foodItemDto)
        {
            var food = await _context.FoodItems.FindAsync(id);
            if (food == null)
            {
                return null;
            }
            food.Name = foodItemDto.Name;
            food.Description = foodItemDto.Description;
            food.Price = foodItemDto.Price;
            food.CategoryId = foodItemDto.CategoryId;
            food.IsAvailable = foodItemDto.IsAvailable;
            if (foodItemDto.Image != null && foodItemDto.Image.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(foodItemDto.Image.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await foodItemDto.Image.CopyToAsync(stream);
                }

                food.ImageUrl = fileName;
            }
            await _context.SaveChangesAsync();
            return food;
            
        }
    }
}