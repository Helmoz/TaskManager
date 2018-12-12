using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task : Identity
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Details { get; set; }

        public List<Tag> Tags { get; set; }

        public List<User> AssignedTo { get; set; }

        public User Creator { get; set; }

        public bool IsCompleted { get; set; }

        public User CompletedBy { get; set; }

        public bool IsPriority { get; set; }

        public bool IsSpecial { get; set; }
    }
}
