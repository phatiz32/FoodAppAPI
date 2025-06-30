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
            var categoryDto = categorys.Select(m => m.ToCategoryDto());
            return Ok(categoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createDto)
        {
            var category = createDto.ToCategoryDto();// chuyen tu dto sang enity
            var createCategory = await _categoryRepo.AddAsync(category);
            return Ok(createCategory.ToCategoryDto());
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

    }
}