using TransactionSystem.Models;

namespace TransactionSystem.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionDbContext _context;
        public IAccountRepository Accounts { get; }
        public IRepository<Deposit> Deposits { get; }
        public IRepository<Withdraw> Withdraws { get; }
        public IRepository<Transfer> Transfers { get; }

        public UnitOfWork(TransactionDbContext context)
        {
            _context = context;

            Accounts = new Repositories.AccountRepository(context);
            Deposits = new Repositories.Repository<Deposit>(context);
            Withdraws = new Repositories.Repository<Withdraw>(context);
            Transfers = new Repositories.Repository<Transfer>(context);
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
