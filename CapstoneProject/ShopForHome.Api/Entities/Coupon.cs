namespace ShopForHome.Api.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = "";
        public decimal DiscountPercent { get; set; }   // Example: 10 = 10% off
        public bool IsActive { get; set; } = true;

        public ICollection<CouponAssignment> Assignments { get; set; } = new List<CouponAssignment>();
    }
}
