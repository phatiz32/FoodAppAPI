using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Dtos.FoodReviews;
using myapi.Interfaces;
using myapi.Mappers;
using myapi.Models;

namespace myapi.Repository
{
    public class FoodReviewRepository : IFoodReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public FoodReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<FoodReviewInfoDto> CreateReviewAsync(string userId, CreateFoodReviewDto dto)
        {
            var review = new FoodReview
            {
                UserId = userId,
                FoodItemId = dto.FoodItemId,
                Comment = dto.Comment,
                Rating = dto.Rating,
            };
            await _context.FoodReviews.AddAsync(review);
            await _context.SaveChangesAsync();
            await _context.Entry(review).Reference(r => r.User).LoadAsync();
            return review.ToFoodReviewInfoDto();
        }

        public async Task<List<FoodReviewInfoDto>> GetReviewsByFoodItemIdAsync(int foodItemId)
        {
            return await _context.FoodReviews.Where(f => f.FoodItemId == foodItemId)
            .Include(f=>f.User).OrderByDescending(f=>f.CreatedAt)
            .Select(f => f.ToFoodReviewInfoDto()).ToListAsync();
        }
    }
}