using Microsoft.CodeAnalysis.CSharp.Syntax;
using myapi.Dtos.Category;
using myapi.Models;

namespace myapi.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Name = category.Name
            };
        }
        public static Category ToCategoryDto(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }

    }
}