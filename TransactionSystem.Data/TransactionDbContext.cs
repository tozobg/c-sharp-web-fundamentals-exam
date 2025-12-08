using Microsoft.EntityFrameworkCore;

using TransactionSystem.Models;

namespace TransactionSystem.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Deposit> Deposits { get; set; } = null!;
        public DbSet<Withdraw> Withdraws { get; set; } = null!;
        public DbSet<Transfer> Transfers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
