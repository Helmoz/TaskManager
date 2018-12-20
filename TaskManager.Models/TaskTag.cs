using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Models
{
    public class TaskTag : Identity
    {
        public Task Task { get; set; }

        public Tag Tag { get; set; }
    }
}
