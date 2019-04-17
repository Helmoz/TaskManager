using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Context.ContextFactory;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private TaskManagerDbContext db;

        private string connection = "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        public ProjectRepository()
        {
            this.db = new TaskManagerDbContextFactory().CreateDbContext(connection);
        }

        public Project AddProject(Project project)
        {
            var entity = db.Projects.Add(project);
            return entity.Entity;
        }

        public Project DeleteProject(Project project)
        {
            var entity = db.Projects.Remove(project);
            return entity.Entity;
        }

        public List<Project> GetProjects()
        {
            return db.Projects.ToList();
        }

        public Project UpdateProject(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
            return db.Entry(project).Entity;
        }
    }
}
