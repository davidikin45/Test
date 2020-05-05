using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Data;
using Test.Dto;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<StatsController> _logger;

        public StatsController(ApplicationDbContext applicationDbContext, ILogger<StatsController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomProgress>> RoomSummary()
        {
            _logger.LogInformation("Obtaining summary of jobs stats per room type...");

            return await _applicationDbContext.RoomProgressStats.FromSqlRaw(@"
                    SELECT 
                    R.Name, 
                    S.Status,
                    Count(*) as Count,
                    Total
                    FROM RX_RoomType R
                    CROSS JOIN (
                    SELECT 'Not Started' as Status
                    UNION
                    SELECT 'Delayed' as Status
                    UNION
                    SELECT 'In Progress' as Status
                    UNION
                    SELECT 'Complete' as Status
                    ) as S
                    LEFT JOIN (
                        SELECT RoomTypeId, Count(*) as Total
                        From RX_Job
                        Group By RoomTypeId
                    ) as T
                    ON R.Id = T.RoomTypeId
                    LEFT JOIN RX_Job J
                    ON J.RoomTypeId = R.Id
                    AND J.Status = S.Status
                    GROUP BY 
                    R.Name, S.Status, T.Total
                    ORDER BY R.Name
                ").ToListAsync();
        }
    }
}
