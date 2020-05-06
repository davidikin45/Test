using Test.Data;

namespace Test.Dto
{
    public class JobUpdateDto
    {
        public JobStatus Status { get; set; }
        public string DelayReason { get; set; }

        public void Update(Job job)
        {
            job.UpdateStatus(Status);
            job.DelayReason = DelayReason;
        }
    }
}
