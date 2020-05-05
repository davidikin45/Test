using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Data
{
    public class RoomType
    {
        public Guid Id { get; set; }
        [MaxLength(28)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
