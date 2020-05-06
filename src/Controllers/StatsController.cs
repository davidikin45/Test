using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Data.Repositories;
using Test.Dto;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsRepository _statsRepository;
        private readonly ILogger<StatsController> _logger;

        public StatsController(IStatsRepository statsRepository, ILogger<StatsController> logger)
        {
            _statsRepository = statsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomProgress>> RoomSummary()
        {
            _logger.LogInformation("Obtaining summary of jobs stats per room type...");

            return await _statsRepository.GetRoomSummaryAsync();
        }
    }
}
