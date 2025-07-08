using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.FoodReviews;

namespace myapi.Interfaces
{
    public interface IFoodReviewRepository
    {
        Task<FoodReviewInfoDto> CreateReviewAsync(string userId, CreateFoodReviewDto dto);
        Task<List<FoodReviewInfoDto>> GetReviewsByFoodItemIdAsync(int foodItemId);
    }
}