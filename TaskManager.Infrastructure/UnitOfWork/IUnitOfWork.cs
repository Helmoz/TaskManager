using System.Threading.Tasks;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        BaseRepository<Project> ProjectRepository { get; }

        BaseRepository<Tag> TagRepository { get; }

        BaseRepository<User> UserRepository { get; }

        BaseRepository<Task> TaskRepository { get; }

        BaseRepository<ProjectMember> ProjectMemberRepository { get; }

        BaseRepository<ProjectTag> ProjectTagRepository { get; }

        BaseRepository<TaskMember> TaskMemberRepository { get; }

        BaseRepository<TaskTag> TaskTagRepository { get; }

        void Dispose();
        Task<int> Save();
    }
}
