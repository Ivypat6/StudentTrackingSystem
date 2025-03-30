using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.Service.Services;
using StudentTrackingSystem.Shared.Dto;

namespace StudentTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public AnalyticsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("predictions")]
        public async Task<ActionResult<IEnumerable<PredictionResultDto>>>
        GetPerformancePredictions()
        {
            var predictions = await _studentService.GetPerformancePredictionsAsync();
            return Ok(predictions);
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardStatsDto>>
    GetDashboardStats()
        {
            var stats = await _studentService.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }

}
