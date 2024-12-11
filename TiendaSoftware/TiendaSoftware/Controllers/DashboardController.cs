using TiendaSoftware.Constants;
using TiendaSoftware.DTOS.Common;
using TiendaSoftware.DTOS.Dashboard;
using TiendaSoftware.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TiendaSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }
        [HttpGet("info")]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<DashboardDto>>> GetDashboardInfo()
        {
            var dashboardResponse = await _dashboardService.GetDashboardAsync();


            return StatusCode(dashboardResponse.StatusCode, dashboardResponse);
        }
    }
}
