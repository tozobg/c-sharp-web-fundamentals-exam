using Microsoft.EntityFrameworkCore;
using TransactionSystem.Core.Services;
using TransactionSystem.Data;
using TransactionSystem.Data.UnitOfWork;

namespace TransactionSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // 1. Get Connection String
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2. Register DbContext
            builder.Services.AddDbContext<TransactionDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 3. Register your Business Logic (Copy these from your old StartUp.cs)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<TransactionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
