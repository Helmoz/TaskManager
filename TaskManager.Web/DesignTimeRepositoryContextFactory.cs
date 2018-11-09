using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Context.ContextFactory;

namespace TaskManager
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
    {
        public TaskManagerDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var repositoryFactory = new TaskManagerDbContextFactory();
            return repositoryFactory.CreateDbContext(connectionString);
        }
    }
}
