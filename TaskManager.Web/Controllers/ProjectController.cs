using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.UnitOfWork;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _unitOfWork.ProjectRepository.Get().Include(x=>x.Tags).Include(x => x.Members).ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProject([FromBody]Project project)
        {
            if (_unitOfWork.ProjectRepository.Get().Any(x => x.Name == project.Name))
                return BadRequest(project);

            var creator = _unitOfWork.UserRepository.Get(x => x.Email == project.Members.FirstOrDefault().Email).FirstOrDefault();

            project.Members = new List<User> {creator};


            _unitOfWork.ProjectRepository.Insert(project);

            await _unitOfWork.Save();

            return Ok(_unitOfWork.ProjectRepository.Get(x=>x.Id == project.Id).Include(x=>x.Tags).FirstOrDefault());
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProject([FromBody]Project updatedProject)
        {
            var project = _unitOfWork.ProjectRepository
                .Get(x => x.Id == updatedProject.Id)
                .Include(x=>x.Tags)
                .Include(x=>x.Members)
                .FirstOrDefault();

            project.Name = updatedProject.Name;

            project.Description = updatedProject.Description;

            project.Type = updatedProject.Type;

            project.Progress = updatedProject.Progress;
            
            project.Deadline = updatedProject.Deadline;
            
            project.Tags.AddRange(updatedProject.Tags.Where(x => x.Id == 0));

            var removedTags = project.Tags.Where(x => !updatedProject.Tags.Contains(x, new TagComparer())).ToList();

            removedTags.ForEach(x =>
            {
                project.Tags.Remove(x);
                _unitOfWork.TagRepository.Delete(x);
            });

            var removedMembers = project.Members.Where(x => !updatedProject.Members.Contains(x, new MemberComparer())).ToList();

            removedMembers.ForEach(x => project.Members.Remove(x));

            var newMembers = updatedProject.Members.Where(x => !project.Members.Contains(x, new MemberComparer())).ToList();
            
            newMembers.ForEach(x => project.Members.Add(_unitOfWork.UserRepository.Get(y=> y.Id == x.Id).FirstOrDefault()));
            
            _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.Save();
            
            return Ok(project);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProject([FromBody] Project project)
        {
            _unitOfWork.ProjectRepository.Delete(project);

            await _unitOfWork.Save();

            return Ok(project);
        }
    }

    public class TagComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Tag obj)
        {
            return (obj.Id * 13 + 29) * 53;
        }
    }

    public class MemberComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(User obj)
        {
            return (obj.Id * 13 + 29) * 53;
        }
    }
}
