using System;
using Test.Data;
using Test.Dto;
using Xunit;

namespace UnitTests.Model
{
    public class MappingTests
    {
        [Fact]
        public void Update_Delayed()
        {
            var dto = new JobUpdateDto() { Status = JobStatus.Delayed, DelayReason = "Other" };
            var job = new Job() { Id = Guid.Parse("D0E96FF7-BB53-472B-A42C-000112B8FCA1"), ContractorID = null, Name = "Job22_25_1", Floor = 22, Room = 25, StatusNum = JobStatus.NotStarted, RJobID = null, RoomTypeId = Guid.Parse("86F23884-1971-4BDF-90DB-629F887ED76A") };
            
            dto.Update(job);

            Assert.Equal(dto.Status, job.StatusNum);
            Assert.Equal(dto.DelayReason, job.DelayReason);
        }
    }
}
