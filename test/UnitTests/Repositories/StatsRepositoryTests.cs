using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Repositories;
using Test.Extensions;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Repositories
{
    public class StatsRepositoryTests
    {
        [Fact]
        public async Task Correct_Stats_Returned()
        {
            using (var contextFactory = new SqliteInMemoryDbContextFactory<ApplicationDbContext>(options => new ApplicationDbContext(options)))
            {
                using (var context = await contextFactory.CreateContextAsync())
                {
                    SeedTestData.Seed(context);
                    await context.SaveChangesAsync();
                }

                using (var context = await contextFactory.CreateContextAsync())
                {
                    var repo = new StatsRepository(context);
                    var results = await repo.GetRoomSummaryAsync();

                    //Ensure the SQL returns correct stats for each room type
                    foreach (var roomType in context.RoomTypes)
                    {
                        Assert.Equal(await context.Jobs.CountAsync(j => j.RoomTypeId == roomType.Id && j.StatusNum == JobStatus.NotStarted), results.First(r => r.Name == roomType.Name && r.Status == JobStatus.NotStarted.Description()).Count);
                        Assert.Equal(await context.Jobs.CountAsync(j => j.RoomTypeId == roomType.Id && j.StatusNum == JobStatus.Delayed), results.First(r => r.Name == roomType.Name && r.Status == JobStatus.Delayed.Description()).Count);
                        Assert.Equal(await context.Jobs.CountAsync(j => j.RoomTypeId == roomType.Id && j.StatusNum == JobStatus.InProgress), results.First(r => r.Name == roomType.Name && r.Status == JobStatus.InProgress.Description()).Count);
                        Assert.Equal(await context.Jobs.CountAsync(j => j.RoomTypeId == roomType.Id && j.StatusNum == JobStatus.Complete), results.First(r => r.Name == roomType.Name && r.Status == JobStatus.Complete.Description()).Count);
                        Assert.Equal(await context.Jobs.CountAsync(j => j.RoomTypeId == roomType.Id), results.First(r => r.Name == roomType.Name).Total);
                    }
                }
            }
        }
    }
}
