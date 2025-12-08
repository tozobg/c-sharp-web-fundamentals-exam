using System;
using System.Threading.Tasks;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.IO.Interfaces;
using TransactionSystem.Models;
using TransactionSystem.Data;


namespace TransactionSystem.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private TransactionDbContext context;

        public Engine(IReader reader, IWriter writer, TransactionDbContext context)
        {
            this.reader = reader;
            this.writer = writer;
            this.context = context;
        }

        // Write code implementation
        public async Task Run()
        {
            using var uow = new UnitOfWork(context);

            // Example: create a new account
            var account = new Account
            {
                AccountNumber = 12345,
                FullName = "John Doe",
                Balance = 0
            };

            await uow.Accounts.AddAsync(account);
            await uow.CompleteAsync();

            writer.WriteLine($"Created account with ID: {account.Id}");
        }
    }
}