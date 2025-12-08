using Microsoft.EntityFrameworkCore;

namespace TransactionSystem.Data.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TransactionDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(TransactionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }
    }
}
