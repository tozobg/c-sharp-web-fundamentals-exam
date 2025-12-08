// This class is only needed if you want to create the database using EF Core migrations.
// To use it:
// 1. Uncomment this file.
// 2. Make sure to comment out `context.Database.EnsureCreated()` in Startup.cs


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
