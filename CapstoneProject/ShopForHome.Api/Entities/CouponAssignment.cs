namespace ShopForHome.Api.Entities
{
    public class CouponAssignment
    {
        public int CouponAssignmentId { get; set; }
        public int CouponId { get; set; }
        public int UserId { get; set; }

        public Coupon? Coupon { get; set; }
        public User? User { get; set; }
    }
}
