using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Context
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ProjectMember> ProjectMembers { get; set; }

        public DbSet<ProjectTag> ProjectTags { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskMember> TaskMembers { get; set; }

        public DbSet<TaskTag> TaskTags { get; set; }
    }
}
