namespace myapi.Helpers
{
    public class QueryObject
    {
        public int PageSize { get; set; } = 8;

        public int PageNumber { get; set; } = 1;
        public string? SearchName { get; set; }
        public int? CategoryId{ get; set; }
    }
}