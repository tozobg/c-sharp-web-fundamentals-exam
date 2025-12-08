using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TransactionSystem.Core;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.Core.Services;
using TransactionSystem.Data;
using TransactionSystem.IO;

namespace TransactionSystem
{
    public class StartUp
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>();
            optionsBuilder.UseSqlite("Data Source=Transactions.db");

            using var context = new TransactionDbContext(optionsBuilder.Options);

            // Ensure the database is created
            context.Database.EnsureCreated();

            UnitOfWork uow = new(context);
            AccountService accountService = new(uow);
            TransactionService transactionService = new(uow);
            TransactionController controller = new(accountService, transactionService);

            // Create engine with reader and writer
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), controller);

            // Start program
            await engine.Run();
        }
    }
}
