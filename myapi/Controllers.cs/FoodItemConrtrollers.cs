using System.Formats.Asn1;
using Microsoft.AspNetCore.Mvc;
using myapi.Dtos.FoodItem;
using myapi.Helpers;
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
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var foods = await _foodRepo.GetAllAsync(query);
            return Ok(foods);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFoodItemDto dto)
        {
            var result = await _foodRepo.CreateAsync(dto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateFoodItemDto dto)
        {
            var fooditem = await _foodRepo.UpdateAsync(id, dto);
            if (fooditem == null)
            {
                return NotFound("Food item not found");
            }
            return Ok(fooditem);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var food = _foodRepo.DeleteAsync(id);
            if (food == null)
            {
                return NotFound("Food item not found");
            }
            return NoContent();
        }

    }
}