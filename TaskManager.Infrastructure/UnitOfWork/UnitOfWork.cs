using System;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Context.ContextFactory;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;

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

        public BaseRepository<Project> ProjectRepository =>
            _projectRepository ?? (_projectRepository = new BaseRepository<Project>(_context));

        public BaseRepository<Tag> TagRepository =>
            _tagRepository ?? (_tagRepository = new BaseRepository<Tag>(_context));

        public BaseRepository<User> UserRepository =>
            _userRepository ?? (_userRepository = new BaseRepository<User>(_context));

        public Task<int> Save() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
