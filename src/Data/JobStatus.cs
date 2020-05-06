using System.ComponentModel.DataAnnotations;

namespace Test.Data
{
    public enum JobStatus
    {
        [Display(Name = "Complete")]
        Complete = 1,
        [Display(Name = "Not Started")]
        NotStarted = 2,
        [Display(Name = "In Progress")]
        InProgress = 3,
        [Display(Name = "Delayed")]
        Delayed = 4
    }
}
