using Microsoft.EntityFrameworkCore;
using System;
using TransactionSystem.Core;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.Data;
using TransactionSystem.IO;

namespace TransactionSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>();
            optionsBuilder.UseSqlite("Data Source=Transactions.db");

            using var context = new TransactionDbContext(optionsBuilder.Options);

            // Ensure the database is created
            context.Database.EnsureCreated();

            // Create engine with reader and writer
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());

            // Start program
            engine.Run();
        }
    }
}
