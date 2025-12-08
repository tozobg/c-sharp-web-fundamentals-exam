using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.Data.InMemory.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        protected readonly ConcurrentDictionary<int, T> _storage = new();
        protected int _currentId = 0;

        public Task AddAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                _currentId++;
                idProperty.SetValue(entity, _currentId);
            }

            _storage[_currentId] = entity;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            _storage.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task RemoveAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var id = (int)idProperty.GetValue(entity)!;
                _storage.TryRemove(id, out _);
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var id = (int)idProperty.GetValue(entity)!;
                _storage[id] = entity;
            }
            return Task.CompletedTask;
        }
    }
}
