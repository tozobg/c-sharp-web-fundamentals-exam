using System;
using System.Threading.Tasks;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.IO.Interfaces;
using TransactionSystem.Models;
using TransactionSystem.Data;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.Utilities;


namespace TransactionSystem.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private TransactionDbContext context;
        private readonly TransactionController _controller;

        public Engine(IReader reader, IWriter writer, TransactionController controller)
        {
            this.reader = reader;
            this.writer = writer;
            this._controller = controller;
        }

        //// Write code implementation
        //public async Task Run()
        //{
        //}
        public async Task Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Show Accounts");
                Console.WriteLine("6. Exit");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await HandleAddAccount();
                            break;

                        case "2":
                            await HandleDeposit();
                            break;

                        case "3":
                            await HandleWithdraw();
                            break;

                        case "4":
                            await HandleTransfer();
                            break;

                        case "5":
                            await HandleShowAccounts();
                            //await _controller.ShowAccountsAsync();
                            break;

                        case "6":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private async Task HandleAddAccount()
        {
            Console.Write("Full Name: ");
            string fullName = Console.ReadLine()!;
            Console.Write("Account Number: ");
            int accNum = int.Parse(Console.ReadLine()!);
            Console.Write("Account Balance: ");
            decimal accBalance = decimal.Parse(Console.ReadLine()!);

            var dto = new CreateAccountDto
            {
                FullName = fullName,
                AccountNumber = accNum,
                Balance = accBalance
            };

            if (!DtoValidator.IsValid(dto))
            {
                return;
            }

            await _controller.AddAccountAsync(dto);
            Console.WriteLine("Account created.");
        }

        private async Task HandleDeposit()
        {
            await _controller.ShowAccountsAsync();
            Console.Write("Account Number: ");
            var accNum = int.Parse(Console.ReadLine()!);
            Console.Write("Amount: ");
            var amount = decimal.Parse(Console.ReadLine()!);

            var dto = new TransactionInputDto
            {
                AccountNumber = accNum,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto)) return;

            await _controller.DepositAsync(dto);
            Console.WriteLine("Deposit successful.");
        }

        private async Task HandleWithdraw()
        {
            await _controller.ShowAccountsAsync();
            Console.Write("Account Number: ");
            var accNum = int.Parse(Console.ReadLine()!);
            Console.Write("Amount: ");
            var amount = decimal.Parse(Console.ReadLine()!);

            var dto = new TransactionInputDto
            {
                AccountNumber = accNum,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto)) return;

            await _controller.WithdrawAsync(dto);
            Console.WriteLine("Withdrawal successful.");
        }

        private async Task HandleTransfer()
        {
            await _controller.ShowAccountsAsync();
            Console.Write("From Account Number: ");
            var from = int.Parse(Console.ReadLine()!);
            Console.Write("To Account Number: ");
            var to = int.Parse(Console.ReadLine()!);
            Console.Write("Amount: ");
            var amount = decimal.Parse(Console.ReadLine()!);

            var dto = new TransferInputDto
            {
                FromAccountNumber = from,
                ToAccountNumber = to,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto)) return;

            await _controller.TransferAsync(dto);
            Console.WriteLine("Transfer successful.");
        }

        private async Task HandleShowAccounts()
        {
            string result = await _controller.ShowAccountsAsync();

            Console.WriteLine(result);
        }
    }
}