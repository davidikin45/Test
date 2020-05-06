using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Controllers;
using Test.Data.Repositories;
using Test.Dto;
using Xunit;

namespace UnitTests.Controllers
{
    public class StatsControllerTests
    {
        [Fact]
        public async Task Ensure_Action_Returns_List_Of_RoomProgress()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<StatsController>();

            var expectedResults = new List<RoomProgress>() { };

            // create mock repository
            var statsRepository = new Mock<IStatsRepository>();
            statsRepository.Setup(s => s.GetRoomSummaryAsync()).ReturnsAsync(expectedResults);

            var controller = new StatsController(statsRepository.Object, logger);
            var results = await controller.RoomSummary();

            Assert.Equal(expectedResults.Count, results.Count());
        }
    }
}
