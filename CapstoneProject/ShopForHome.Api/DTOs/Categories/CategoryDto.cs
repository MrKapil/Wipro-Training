using System; 
using System.Collections.Generic;


namespace ShopForHome.Api.DTOs.Categories
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
    }
}
