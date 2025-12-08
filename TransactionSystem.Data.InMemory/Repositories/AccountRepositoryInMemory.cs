using TransactionSystem.Models;

namespace TransactionSystem.Data.InMemory.Repositories
{
    public class AccountRepositoryInMemory : InMemoryRepository<Account>, IAccountRepository
    {
        public Task<Account?> GetByAccountNumberAsync(int accountNumber)
        {
            var account = _storage.Values.FirstOrDefault(a => a.AccountNumber == accountNumber);
            return Task.FromResult(account);
        }
    }
}
