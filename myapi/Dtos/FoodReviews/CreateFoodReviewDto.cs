using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Dtos.FoodReviews
{
    public  class CreateFoodReviewDto
    {
        public int FoodItemId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}