using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Test.Controllers;
using Test.Data;
using Test.Dto;
using Xunit;

namespace UnitTests.Controllers
{
    public class JobControllerTests
    {
        private async Task SeedDatabase(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                SeedTestData.Seed(context);

                await context.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task Update_Valid_ReturnsNoContentResult()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<JobController>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Update_NotStarted_ReturnsNoContentResult").Options;

            await SeedDatabase(options);

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new JobController(context, logger);
                var dto = new JobUpdateDto() { Status = JobStatus.NotStarted };
                var result = await controller.Update(Guid.Parse("A5F29486-04F2-439A-979D-0A3F9E323B92"), dto);
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task Update_Invalid_ReturnsNotFoundResult()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<JobController>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Update_Invalid_ReturnsNotFoundResult").Options;

            await SeedDatabase(options);

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new JobController(context, logger);
                var dto = new JobUpdateDto() { Status = JobStatus.NotStarted };
                var result = await controller.Update(Guid.Parse("EC572A33-1D58-4671-870E-BC4756F79EE4"), dto);
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
