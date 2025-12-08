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
        public void Update(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    }
}
