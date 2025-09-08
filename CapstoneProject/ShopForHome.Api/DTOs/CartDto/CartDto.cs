namespace ShopForHome.Api.DTOs.Cart
{


    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }


}
