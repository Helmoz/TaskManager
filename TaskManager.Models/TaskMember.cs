using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    public class TaskMember : Identity
    {
        public Task Task { get; set; }

        public User Member { get; set; }
    }
}
