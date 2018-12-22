using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.UnitOfWork;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Project LoadProject(int projectId)
        {
            return _unitOfWork.ProjectRepository
                .Get(x => x.Id == projectId)
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
                .FirstOrDefault();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTask([FromBody]Task task)
        {
            task.Creator = _unitOfWork.UserRepository.Get(x => x.UId == task.Creator.UId).FirstOrDefault();

            var tags = task.Tags.Select(y => y.Tag);

            var tagsFromBd = _unitOfWork.TagRepository.Get(x => tags.Contains(x));

            var taskTags = tagsFromBd.Select(x => new TaskTag() {Tag = x}).ToList();

            task.Tags = taskTags;

            var assigned = task.AssignedTo.Select(x => x.Member);

            var assignedFromBd = _unitOfWork.UserRepository.Get(x => assigned.Contains(x));

            var taskMembers = assignedFromBd.Select(x => new TaskMember() {Member = x}).ToList();

            task.AssignedTo = taskMembers;

            _unitOfWork.TaskRepository.Insert(task);

            await _unitOfWork.Save();

            return Ok(LoadProject(task.ProjectId));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTask([FromBody]Task updatedTask)
        {
            var task = _unitOfWork.TaskRepository.Get(x => x.Id == updatedTask.Id).FirstOrDefault();

            task.Name = updatedTask.Name;

            task.Details = updatedTask.Details;

            task.IsPriority = updatedTask.IsPriority;

            task.IsSpecial = updatedTask.IsSpecial;

            _unitOfWork.TaskRepository.Update(task);

            await _unitOfWork.Save();

           return Ok(LoadProject(task.ProjectId));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> DeleteTask([FromBody] Task deletedTask)
        {
            var task = _unitOfWork.TaskRepository.Get(x => x.Id == deletedTask.Id).FirstOrDefault();

            _unitOfWork.TaskRepository.Delete(task);

            await _unitOfWork.Save();

            return Ok(LoadProject(task.ProjectId));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> CompleteChangeTask([FromBody]Task updatedTask)
        {
            var task = _unitOfWork.TaskRepository.Get(x => x.Id == updatedTask.Id).FirstOrDefault();

            if (!task.IsCompleted)
            {
                var completedBy = _unitOfWork.UserRepository.Get(x => x.UId == updatedTask.CompletedBy.UId).FirstOrDefault();

                task.CompletedBy = completedBy;
            }

            else
            {
                _unitOfWork.TaskRepository.SetNull(task, "CompletedById");
            }

            task.IsCompleted = !task.IsCompleted;

            _unitOfWork.TaskRepository.Update(task);

            await _unitOfWork.Save();
            
            return Ok(LoadProject(task.ProjectId));
        }
    }
}
