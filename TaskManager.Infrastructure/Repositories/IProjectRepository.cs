using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();
        Project AddProject(Project project);
        Project UpdateProject(Project project);
        Project DeleteProject(Project project);
    }
}
