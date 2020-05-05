using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Data
{
    public class Job
    {
        public Guid Id { get; set; }
        public Nullable<int> ContractorID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Room { get; set; }
        [MaxLength(50)]
        public string DelayReason { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateCompleted { get; set; }
        public Nullable<DateTime> DateDelayed { get; set; }
        public Nullable<int> StatusNum { get; set; }
        public Nullable<int> RJobID { get; set; }
        public Guid RoomTypeId { get; set; }
    }
}
