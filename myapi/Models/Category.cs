namespace myapi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<FoodItem>? FoodItems { get; set; }
    }
}