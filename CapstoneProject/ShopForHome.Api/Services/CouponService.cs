using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Coupons;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class CouponService
    {
        private readonly AppDbContext _db;
        public CouponService(AppDbContext db) { _db = db; }

        // Admin: Create coupon
        public async Task<CouponDto> CreateAsync(string code, decimal discountPercent)
        {
            var c = new Coupon { Code = code, DiscountPercent = discountPercent, IsActive = true };
            _db.Coupons.Add(c);
            await _db.SaveChangesAsync();

            return new CouponDto { CouponId = c.CouponId, Code = c.Code, DiscountPercent = c.DiscountPercent };
        }

        // Admin: Assign coupon to a user
        public async Task AssignToUserAsync(int couponId, int userId)
        {
            if (!_db.Coupons.Any(c => c.CouponId == couponId)) throw new Exception("Coupon not found");
            _db.CouponAssignments.Add(new CouponAssignment { CouponId = couponId, UserId = userId });
            await _db.SaveChangesAsync();
        }

        // User: Get assigned coupons
        public async Task<List<CouponDto>> GetUserCouponsAsync(int userId)
        {
            return await _db.CouponAssignments
                .Include(a => a.Coupon)
                .Where(a => a.UserId == userId && a.Coupon!.IsActive)
                .Select(a => new CouponDto
                {
                    CouponId = a.Coupon!.CouponId,
                    Code = a.Coupon.Code,
                    DiscountPercent = a.Coupon.DiscountPercent
                })
                .ToListAsync();
        }

        // User: Apply coupon by code
        public async Task<decimal?> ApplyCouponAsync(int userId, string code, decimal orderTotal)
        {
            var coupon = await _db.CouponAssignments
                .Include(a => a.Coupon)
                .Where(a => a.UserId == userId && a.Coupon!.IsActive && a.Coupon.Code == code)
                .Select(a => a.Coupon)
                .FirstOrDefaultAsync();

            if (coupon == null) return null;

            var discount = (orderTotal * coupon.DiscountPercent) / 100;
            return orderTotal - discount;
        }
    }
}
