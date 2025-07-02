using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using myapi.Dtos.Category;
using myapi.Interfaces;
using myapi.Mappers;

namespace myapi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryReposiory _categoryRepo;
        public CategoryController(ICategoryReposiory categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorys = await _categoryRepo.GetAllAsync();
            var categoryDtos = categorys.Select(c => c.ToshowCategoryDto()).ToList();
            foreach (var cat in categoryDtos)
            {
                Console.WriteLine($"Id: {cat.Id}, Name: {cat.Name}");
            }
            return Ok(categoryDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createDto)
        {
            var category = createDto.ToCategoryDto();// chuyen tu dto sang enity
            var createCategory = await _categoryRepo.AddAsync(category);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _categoryRepo.DeleteAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("raw")]
        public async Task<IActionResult> GetRaw()
        {
            var categorys = await _categoryRepo.GetAllAsync();
            return Ok(categorys);
        }

    }
}