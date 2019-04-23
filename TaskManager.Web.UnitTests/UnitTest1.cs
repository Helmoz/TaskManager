using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Controllers;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void GetProjects_Returns_AListOfProjects()
        {
            var mock = new Mock<IProjectRepository>();
            mock.Setup(a => a.GetProjects()).Returns(GetProjects());

            var controller = new ProjectController(mock.Object);

            // Act
            var result = controller.GetAllProjects();

            // Assert
            Assert.AreEqual(2, result.Count());
        }        

        private List<Project> GetProjects()
        {
            var projects = new List<Project>
            {
                new Project()
                {
                    Name = "project1",
                    Description = "Description 1",
                    Type = Type.Idea
                },
                new Project()
                {
                    Name = "project2",
                    Description = "Description 2",
                    Type = Type.Completed
                }
            };
            return projects;
        }

        private Project CreateNewProject()
        {
            var project = new Project()
            {
                Name = "testName"
            };
            return project;
        }
    }
}