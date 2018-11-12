using System.Threading.Tasks;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;

namespace TaskManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        BaseRepository<Project> ProjectRepository { get; }

        BaseRepository<Tag> TagRepository { get; }

        void Dispose();
        Task<int> Save();
    }
}
