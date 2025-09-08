using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Reports;
using ShopForHome.Api.Services;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _svc;
        public ReportsController(ReportService svc) { _svc = svc; }

        [HttpPost("sales")]
        public async Task<IActionResult> GetSalesReport([FromBody] SalesReportRequest req)
        {
            var report = await _svc.GenerateAsync(req.From, req.To);
            return Ok(report);
        }
    }
}
