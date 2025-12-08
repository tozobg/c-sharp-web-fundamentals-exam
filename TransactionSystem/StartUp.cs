using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TransactionSystem.Core;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.Core.Services;
using TransactionSystem.Data;
using TransactionSystem.Data.InMemory.Repositories;
using TransactionSystem.Data.InMemory.UnitOfWork;
using TransactionSystem.Data.UnitOfWork;
using TransactionSystem.IO;
using TransactionSystem.IO.Interfaces;
using TransactionSystem.Models;

namespace TransactionSystem
{
    public class StartUp
    {
        static async Task Main(string[] args)
        {
            // *** SWITCH HERE ***
            bool useInMemory = false;  // <-- CHANGE THIS TO SWITCH InMemory and SQLite

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices((context, services) =>
                {

                    if (useInMemory)
                    {
                        // Register InMemory Repositories
                        services.AddSingleton<IRepository<Account>, InMemoryRepository<Account>>();
                        services.AddSingleton<IRepository<Deposit>, InMemoryRepository<Deposit>>();
                        services.AddSingleton<IRepository<Withdraw>, InMemoryRepository<Withdraw>>();
                        services.AddSingleton<IRepository<Transfer>, InMemoryRepository<Transfer>>();

                        // Register InMemory Unit of Work
                        services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
                    }
                    else
                    {
                        // Register SQLite DbContext
                        services.AddDbContext<TransactionDbContext>(options =>
                        {
                            options.UseSqlite("Data Source=Transactions.db");

                            // Disable EF Core SQL logging
                            options.LogTo(_ => { });
                        });

                        // Register Unit of Work
                        services.AddScoped<IUnitOfWork, UnitOfWork>();
                    }

                    // Services
                    services.AddScoped<AccountService>();
                    services.AddScoped<TransactionService>();

                    // Reader/Writer
                    services.AddScoped<IReader, ConsoleReader>();
                    services.AddScoped<IWriter, ConsoleWriter>();

                    // Controller
                    services.AddScoped<TransactionController>();

                    // Engine
                    services.AddScoped<Engine>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                // Ensure DB exists when using SQLite
                if (!useInMemory)
                {
                    var db = provider.GetRequiredService<TransactionDbContext>();
                    db.Database.EnsureCreated();
                }

                var engine = provider.GetRequiredService<Engine>();
                await engine.Run();
            }
        }
    }
}
