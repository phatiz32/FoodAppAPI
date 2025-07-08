using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myapi.Dtos.FoodItem;
using myapi.Dtos.FoodReviews;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Controllers.cs
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoodReviewController : ControllerBase
    {
        private readonly IFoodReviewRepository _foodReviewRepos;
        private readonly UserManager<AppUser> _userManager;
        public FoodReviewController(IFoodReviewRepository foodReviewRepos, UserManager<AppUser> userManager)
        {
            _foodReviewRepos = foodReviewRepos;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateReviews([FromBody] CreateFoodReviewDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var result = await _foodReviewRepos.CreateReviewAsync(user.Id, dto);
            return Ok(result);

        }
        [HttpGet("{foodItemId}")]
        public async Task<IActionResult> GetReviewsByFoodItem([FromRoute] int foodItemId)
        {
            var result = await _foodReviewRepos.GetReviewsByFoodItemIdAsync(foodItemId);
            return Ok(result);
        }

    }
}