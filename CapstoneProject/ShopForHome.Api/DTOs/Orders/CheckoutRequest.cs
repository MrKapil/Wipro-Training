namespace ShopForHome.Api.DTOs.Orders
{
    public class CheckoutRequest
    {
        public string ShippingAddress { get; set; } = "";
        // public int? CouponId { get; set; } // optional coupon applied by user
    }
}
