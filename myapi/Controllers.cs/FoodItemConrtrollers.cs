using Microsoft.AspNetCore.Mvc;
using myapi.Dtos.FoodItem;
using myapi.Interfaces;

namespace myapi.Controllers
{
    [Route("api/fooditem")]
    [ApiController]
    public class FoodItemConrtrollers : ControllerBase
    {
        private readonly IFoodItemRepository _foodRepo;
        public FoodItemConrtrollers(IFoodItemRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFoodItemDto dto)
        {
            var result = await _foodRepo.CreateAsync(dto);
            return Ok(result);
        }

    }
}