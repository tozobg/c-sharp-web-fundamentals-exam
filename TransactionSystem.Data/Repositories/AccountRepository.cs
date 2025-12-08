using Microsoft.EntityFrameworkCore;

using TransactionSystem.Models;

namespace TransactionSystem.Data.Repositories
{
    internal class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(TransactionDbContext context) : base(context) { }

        public async Task<Account?> GetByAccountNumberAsync(int accountNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }
    }
}
