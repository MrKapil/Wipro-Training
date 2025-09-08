namespace ShopForHome.Api.DTOs.Categories
{
    public class CategoryCreateUpdateDto
    {
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
        public bool IsActive { get; set; } = true;
    }
}