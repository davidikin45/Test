using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Test.Data;
using Test.Dto;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<JobController> _logger;

        public JobController(ApplicationDbContext applicationDbContext, ILogger<JobController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        //JsonStringEnumConverter allows JobStatus enum to be sent as Complete, NotStarted, InProgress, Delayed
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JobUpdateDto dto)
        {
            var job = await _applicationDbContext.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();

            dto.Update(job);

            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
