using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Cart;
using ShopForHome.Api.Services;
using System.Security.Claims;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // cart requires logged-in user
    public class CartController : ControllerBase
    {
        private readonly CartService _svc;
        public CartController(CartService svc) { _svc = svc; }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _svc.GetCartAsync(GetUserId());
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddToCartRequest req)
        {
            await _svc.AddToCartAsync(GetUserId(), req);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCartRequest req)
        {
            var success = await _svc.UpdateCartItemAsync(GetUserId(), req);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var success = await _svc.RemoveItemAsync(GetUserId(), id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
