using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Coupons;
using ShopForHome.Api.Services;
using System.Security.Claims;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly CouponService _svc;
        public CouponsController(CouponService svc) { _svc = svc; }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Admin: Create coupon
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromQuery] string code, [FromQuery] decimal discountPercent)
        {
            var c = await _svc.CreateAsync(code, discountPercent);
            return Ok(c);
        }

        // Admin: Assign coupon to user
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign([FromQuery] int couponId, [FromQuery] int userId)
        {
            await _svc.AssignToUserAsync(couponId, userId);
            return Ok("Assigned");
        }

        // User: View coupons
        [HttpGet("my")]
        public async Task<IActionResult> MyCoupons()
        {
            var items = await _svc.GetUserCouponsAsync(GetUserId());
            return Ok(items);
        }

        // User: Apply coupon
        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromBody] ApplyCouponRequest req, [FromQuery] decimal orderTotal)
        {
            var discounted = await _svc.ApplyCouponAsync(GetUserId(), req.Code, orderTotal);
            if (discounted == null) return BadRequest("Invalid or unassigned coupon.");
            return Ok(new { Original = orderTotal, Final = discounted });
        }
    }
}
