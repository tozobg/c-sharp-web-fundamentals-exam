using TransactionSystem.Models;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    IRepository<Deposit> Deposits { get; }
    IRepository<Withdraw> Withdraws { get; }
    IRepository<Transfer> Transfers { get; }
    Task<int> CompleteAsync(); // saves changes
}