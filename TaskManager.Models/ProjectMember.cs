using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    public class ProjectMember : Identity
    {
        public Project Project { get; set; }

        public User Member { get; set; }
    }
}
