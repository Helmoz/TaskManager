using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    public class ProjectTag : Identity
    {
        public Project Project { get; set; }

        public Tag Tag { get; set; }
    }
}
