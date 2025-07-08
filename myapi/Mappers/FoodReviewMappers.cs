using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myapi.Dtos.FoodReviews;
using myapi.Models;

namespace myapi.Mappers
{
    public static class FoodReviewMappers
    {
         public static FoodReviewInfoDto ToFoodReviewInfoDto(this FoodReview review)
        {
            return new FoodReviewInfoDto
            {
                UserName = review.User?.FullName,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt
            };
        }
        
    }
}