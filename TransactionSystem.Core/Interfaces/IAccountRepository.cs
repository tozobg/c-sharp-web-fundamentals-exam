using TransactionSystem.Models;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByAccountNumberAsync(int accountNumber);
}