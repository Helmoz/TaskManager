using System;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Context.ContextFactory;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly TaskManagerDbContext _context;

        public UnitOfWork(string connectionString, ITaskManagerDbContextFactory context)
        {
            _context = context.CreateDbContext(connectionString);
        }

        private BaseRepository<Project> _projectRepository;

        private BaseRepository<Tag> _tagRepository;

        private BaseRepository<User> _userRepository;

        private BaseRepository<Task> _taskRepository;

        private BaseRepository<ProjectMember> _projectMemberRepository;

        private BaseRepository<ProjectTag> _projectTagRepository;

        private BaseRepository<TaskMember> _taskMemberRepository;

        private BaseRepository<TaskTag> _taskTagRepository;

        public BaseRepository<Project> ProjectRepository =>
            _projectRepository ?? (_projectRepository = new BaseRepository<Project>(_context));

        public BaseRepository<Tag> TagRepository =>
            _tagRepository ?? (_tagRepository = new BaseRepository<Tag>(_context));

        public BaseRepository<User> UserRepository =>
            _userRepository ?? (_userRepository = new BaseRepository<User>(_context));
        
        public BaseRepository<Task> TaskRepository =>
            _taskRepository ?? (_taskRepository = new BaseRepository<Task>(_context));

        public BaseRepository<ProjectMember> ProjectMemberRepository =>
            _projectMemberRepository ?? (_projectMemberRepository = new BaseRepository<ProjectMember>(_context));

        public BaseRepository<ProjectTag> ProjectTagRepository =>
            _projectTagRepository ?? (_projectTagRepository = new BaseRepository<ProjectTag>(_context));

        public BaseRepository<TaskMember> TaskMemberRepository =>
            _taskMemberRepository ?? (_taskMemberRepository = new BaseRepository<TaskMember>(_context));

        public BaseRepository<TaskTag> TaskTagRepository =>
            _taskTagRepository ?? (_taskTagRepository = new BaseRepository<TaskTag>(_context));

        public Task<int> Save() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
