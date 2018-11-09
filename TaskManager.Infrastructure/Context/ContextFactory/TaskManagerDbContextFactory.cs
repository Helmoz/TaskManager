using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Context.ContextFactory
{
    public class TaskManagerDbContextFactory : ITaskManagerDbContextFactory
    {
        public TaskManagerDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new TaskManagerDbContext(optionsBuilder.Options);
        }
    }
}
