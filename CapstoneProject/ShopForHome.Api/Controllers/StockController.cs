using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Stock;
using ShopForHome.Api.Services;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly StockService _svc;
        public StockController(StockService svc) { _svc = svc; }

        // Admin + user: list all inventory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inv = await _svc.GetAllInventoryAsync();
            return Ok(inv);
        }

        // Admin: get low stock items (threshold query param default 10)
        [HttpGet("low")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLow([FromQuery] int threshold = 10)
        {
            var low = await _svc.GetLowStockAsync(threshold);
            return Ok(low);
        }

        // Admin: list in-memory alerts
        [HttpGet("alerts")]
        [Authorize(Roles = "Admin")]
        public IActionResult Alerts()
        {
            var alerts = _svc.GetAlerts();
            return Ok(alerts);
        }

        // Admin: acknowledge alert
        [HttpPost("alerts/ack/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Ack(string id)
        {
            var ok = _svc.AcknowledgeAlert(id);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
