using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Wishlist;
using ShopForHome.Api.Services;
using System.Security.Claims;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // user must be logged in
    public class WishlistController : ControllerBase
    {
        private readonly WishlistService _svc;
        public WishlistController(WishlistService svc) { _svc = svc; }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var items = await _svc.GetWishlistAsync(GetUserId());
            return Ok(items);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddToWishlistRequest req)
        {
            var success = await _svc.AddToWishlistAsync(GetUserId(), req.ProductId);
            if (!success) return BadRequest("Product already in wishlist.");
            return Ok(new { message = "Added to wishlist." });
        }

        [HttpDelete("remove/{productId:long}")]
        public async Task<IActionResult> Remove(long productId)
        {
            var success = await _svc.RemoveFromWishlistAsync(GetUserId(), productId);
            if (!success) return NotFound();
            return Ok(new { message = "Removed from wishlist." });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Clear()
        {
            await _svc.ClearWishlistAsync(GetUserId());
            return Ok(new { message = "Wishlist cleared." });
        }
    }
}
