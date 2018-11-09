namespace TaskManager.Infrastructure.Context.ContextFactory
{
    public interface ITaskManagerDbContextFactory
    {
        TaskManagerDbContext CreateDbContext(string connectionString);
    }
}
