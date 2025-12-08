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
using TransactionSystem.IO;
using TransactionSystem.IO.Interfaces;

namespace TransactionSystem
{
    public class StartUp
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices((context, services) =>
                {
                    // DbContext
                    services.AddDbContext<TransactionDbContext>(options =>
                    {
                        options.UseSqlite("Data Source=Transactions.db");

                        // Disable EF Core SQL logging
                        options.LogTo(_ => { });
                    });

                    // Unit of Work
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            // Ensure DB exists
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var db = provider.GetRequiredService<TransactionDbContext>();
                db.Database.EnsureCreated();

                var engine = provider.GetRequiredService<Engine>();
                await engine.Run();
            }
        }
    }
}
