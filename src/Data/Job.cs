using System;
using System.ComponentModel.DataAnnotations;
using Test.Extensions;

namespace Test.Data
{
    public class Job
    {
        public Guid Id { get; set; }
        public Nullable<int> ContractorID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Status { get; set; } = JobStatus.NotStarted.Description();
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Room { get; set; }
        [MaxLength(50)]
        public string DelayReason { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateCompleted { get; set; }
        public Nullable<DateTime> DateDelayed { get; set; }

        public JobStatus StatusNum { get; set; } = JobStatus.NotStarted;

        public Nullable<int> RJobID { get; set; }
        public Guid RoomTypeId { get; set; }

        public Job()
        {
            DateCreated = DateTime.UtcNow;
        }

        public void UpdateStatus(JobStatus value)
        {
            StatusNum = value;
            Status = value.Description();
            if (value == JobStatus.Complete)
                DateCompleted = DateTime.UtcNow;

            if (value == JobStatus.Delayed)
                DateDelayed = DateTime.UtcNow;
        }
    }
}
