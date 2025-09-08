namespace ShopForHome.Api.DTOs.Coupons
{
    public class CreateCouponRequest
    {
        public string Code { get; set; } = "";
        public decimal DiscountPercent { get; set; }
    }
}
