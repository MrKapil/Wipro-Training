namespace ShopForHome.Api.DTOs.Coupons
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = "";
        public decimal DiscountPercent { get; set; }
    }
}

