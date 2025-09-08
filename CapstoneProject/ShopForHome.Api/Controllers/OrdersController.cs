using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Orders;
using ShopForHome.Api.Services;
using System.Security.Claims;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _svc;
        public OrdersController(OrderService svc) { _svc = svc; }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            var order = await _svc.CheckoutAsync(GetUserId(), req);
            if (order == null) return BadRequest("Cart is empty.");
            return Ok(order);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _svc.GetUserOrdersAsync(GetUserId());
            return Ok(orders);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _svc.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}
