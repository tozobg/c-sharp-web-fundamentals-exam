//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace TransactionSystem.Data
//{
//    public class TransactionDbContextFactory : IDesignTimeDbContextFactory<TransactionDbContext>
//    {
//        public TransactionDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>();
//            optionsBuilder.UseSqlite("Data Source=Transactions.db");

//            return new TransactionDbContext(optionsBuilder.Options);
//        }
//    }
//}
