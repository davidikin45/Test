using System;
using Test.Data;
using Test.Extensions;
using Xunit;

namespace UnitTests.Model
{
    public class JobTests
    {
        [Fact]
        public void Update_NotStarted()
        {
            var job = new Job() { Id = Guid.Parse("D0E96FF7-BB53-472B-A42C-000112B8FCA1"), ContractorID = null, Name = "Job22_25_1", Floor = 22, Room = 25, StatusNum = JobStatus.NotStarted, RJobID = null, RoomTypeId = Guid.Parse("86F23884-1971-4BDF-90DB-629F887ED76A") };
            job.UpdateStatus(JobStatus.NotStarted);

            Assert.Equal(JobStatus.NotStarted, job.StatusNum);
            Assert.Equal(JobStatus.NotStarted.Description(), job.Status);
            Assert.Null(job.DateDelayed);
            Assert.Null(job.DateCompleted);
        }

        [Fact]
        public void Update_Delayed()
        {
            var job = new Job() { Id = Guid.Parse("D0E96FF7-BB53-472B-A42C-000112B8FCA1"), ContractorID = null, Name = "Job22_25_1", Floor = 22, Room = 25, StatusNum = JobStatus.NotStarted, RJobID = null, RoomTypeId = Guid.Parse("86F23884-1971-4BDF-90DB-629F887ED76A") };
            job.DelayReason = "Other";
            job.UpdateStatus(JobStatus.Delayed);

            Assert.Equal(JobStatus.Delayed, job.StatusNum);
            Assert.Equal(JobStatus.Delayed.Description(), job.Status);
            Assert.NotNull(job.DateDelayed);
            Assert.Null(job.DateCompleted);
            Assert.Equal("Other", job.DelayReason);
        }

        [Fact]
        public void Update_InProgress()
        {
            var job = new Job() { Id = Guid.Parse("D0E96FF7-BB53-472B-A42C-000112B8FCA1"), ContractorID = null, Name = "Job22_25_1", Floor = 22, Room = 25, StatusNum = JobStatus.NotStarted, RJobID = null, RoomTypeId = Guid.Parse("86F23884-1971-4BDF-90DB-629F887ED76A") };
            job.UpdateStatus(JobStatus.InProgress);

            Assert.Equal(JobStatus.InProgress, job.StatusNum);
            Assert.Equal(JobStatus.InProgress.Description(), job.Status);
            Assert.Null(job.DateDelayed);
            Assert.Null(job.DateCompleted);
        }

        [Fact]
        public void Update_Complete()
        {
            var job = new Job() { Id = Guid.Parse("D0E96FF7-BB53-472B-A42C-000112B8FCA1"), ContractorID = null, Name = "Job22_25_1", Floor = 22, Room = 25, StatusNum = JobStatus.NotStarted, RJobID = null, RoomTypeId = Guid.Parse("86F23884-1971-4BDF-90DB-629F887ED76A") };
            job.UpdateStatus(JobStatus.Complete);

            Assert.Equal(JobStatus.Complete, job.StatusNum);
            Assert.Equal(JobStatus.Complete.Description(), job.Status);
            Assert.NotNull(job.DateCompleted);
        }
    }
}
