namespace ShopForHome.Api.Entities
{
    public class AppliedCoupon
    {
        public int AppliedCouponId { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }
        public DateTime AppliedDate { get; set; }

        public User? User { get; set; }
        public Coupon? Coupon { get; set; }
    }
}