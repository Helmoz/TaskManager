using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum Type
    {
        Idea, Development, Completed
    }

    public class Project : Identity
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        
        public int Progress { get; set; }

        public DateTime Deadline { get; set; }

        [Required]
        public Type Type { get; set; }

        public List<Tag> Tags { get; set; }

        public List<User> Members { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
