using System.ComponentModel.DataAnnotations;

namespace myapi.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}