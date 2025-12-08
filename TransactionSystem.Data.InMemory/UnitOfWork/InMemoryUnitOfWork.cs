using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Data.InMemory.Repositories;
using TransactionSystem.Models;

namespace TransactionSystem.Data.InMemory.UnitOfWork
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public IAccountRepository Accounts { get; }
        public IRepository<Deposit> Deposits { get; }
        public IRepository<Withdraw> Withdraws { get; }
        public IRepository<Transfer> Transfers { get; }

        public InMemoryUnitOfWork()
        {
            Accounts = new AccountRepositoryInMemory();
            Deposits = new InMemoryRepository<Deposit>();
            Withdraws = new InMemoryRepository<Withdraw>();
            Transfers = new InMemoryRepository<Transfer>();
        }

        public Task CompleteAsync()
        {
            // Nothing to persist, in-memory is immediate
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
