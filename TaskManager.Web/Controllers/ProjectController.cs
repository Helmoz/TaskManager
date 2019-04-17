using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.UnitOfWork;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _repository;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }


        private Project LoadProject(int projectId)
        {
            return _unitOfWork.ProjectRepository
                .Get(x=>x.Id == projectId)
                .Include(x => x.Members)
                    .ThenInclude(x => x.Member)
                .Include(x => x.Tags)
                    .ThenInclude(x => x.Tag)
                .Include(x => x.Tasks)
                    .ThenInclude(x => x.Tags)
                    .ThenInclude(x=>x.Tag)
                .Include(x => x.Tasks)
                    .ThenInclude(x => x.AssignedTo)
                    .ThenInclude(x=>x.Member)
                .FirstOrDefault();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            var a = await _unitOfWork.ProjectRepository
                .Get()
                .Include(x => x.Members)
                .ThenInclude(x => x.Member)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Tasks)
                .ThenInclude(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Tasks)
                .ThenInclude(x => x.AssignedTo)
                .ThenInclude(x => x.Member)
                .ToListAsync();
            return a;
        }
        public IEnumerable<Project> GetAllProjects()
        {
            var a = _repository.GetProjects();
            return a;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProject([FromBody]Project project)
        {
            if (_unitOfWork.ProjectRepository.Get().Any(x => x.Name == project.Name))
                return BadRequest(project);

            var creator = _unitOfWork.UserRepository.Get(x => x.Email == project.Members.FirstOrDefault().Member.Email).FirstOrDefault();

            project.Members = new List<ProjectMember> {new ProjectMember() {Member = creator}};

            _unitOfWork.ProjectRepository.Insert(project);

            await _unitOfWork.Save();

            return Ok(LoadProject(project.Id));
        }       

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProject([FromBody]Project updatedProject)
        {
            var project = LoadProject(updatedProject.Id);

            project.Name = updatedProject.Name;

            project.Description = updatedProject.Description;

            project.Type = updatedProject.Type;

            project.Progress = updatedProject.Progress;

            project.Deadline = updatedProject.Deadline;

            project.Tags.AddRange(updatedProject.Tags.Where(x => x.Id == 0));

            var removedTags = project.Tags.Where(x => !updatedProject.Tags.Contains(x, new ProjectTagComparer())).ToList();

            removedTags.ForEach(x =>
            {
                project.Tags.Remove(x);
                _unitOfWork.ProjectTagRepository.Delete(x);
            });

            var removedMembers = project.Members.Where(x => !updatedProject.Members.Contains(x, new ProjectMemberComparer())).ToList();

            removedMembers.ForEach(x =>
            {
                project.Members.Remove(x);
                _unitOfWork.ProjectMemberRepository.Delete(x);
            });

            var newMembers = updatedProject.Members.Where(x => !project.Members.Contains(x, new ProjectMemberComparer())).ToList();

            newMembers.ForEach(x =>
            {
                var user = _unitOfWork.UserRepository.Get(y => y.Id == x.Member.Id).FirstOrDefault();
                var member = new ProjectMember() {Member =  user};
                project.Members.Add(member);
            });

            _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.Save();

            return Ok(LoadProject(project.Id));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProject([FromBody] Project project)
        {
            _unitOfWork.ProjectRepository.Delete(project);

            await _unitOfWork.Save();

            return Ok(project);
        }
    }


    public class ProjectTagComparer : IEqualityComparer<ProjectTag>
    {
        public bool Equals(ProjectTag x, ProjectTag y)
        {
            return x.Id == y.Id && x.Tag.Name == y.Tag.Name;
        }

        public int GetHashCode(ProjectTag obj)
        {
            return (obj.Id * 13 + 29) * 53;
        }
    }

    public class ProjectMemberComparer : IEqualityComparer<ProjectMember>
    {
        public bool Equals(ProjectMember x, ProjectMember y)
        {
            return x.Id == y.Id && x.Member.Name == y.Member.Name;
        }

        public int GetHashCode(ProjectMember obj)
        {
            return (obj.Id * 13 + 29) * 53;
        }
    }
}
