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
    /// <summary>
    /// UI of app using Console for input and output. Having interactive menu and handlers for options.
    /// </summary>
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private readonly TransactionController _controller;

        public Engine(IReader reader, IWriter writer, TransactionController controller)
        {
            this.reader = reader;
            this.writer = writer;
            this._controller = controller;
        }

        public async Task Run()
        {
            while (true)
            {
                writer.WriteLine("\n--- Menu ---");
                writer.WriteLine("1. Add Account");
                writer.WriteLine("2. Deposit");
                writer.WriteLine("3. Withdraw");
                writer.WriteLine("4. Transfer");
                writer.WriteLine("5. Show Accounts");
                writer.WriteLine("6. Exit");
                writer.Write("Choice: ");
                var choice = reader.ReadLine();

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
                            break;

                        case "6":
                            return;

                        default:
                            writer.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private async Task HandleAddAccount()
        {
            writer.Write("Full Name: ");
            string fullName = reader.ReadLine()!;

            writer.Write("Account Number: ");
            int accNum = int.Parse(reader.ReadLine()!);

            writer.Write("Account Balance: ");
            decimal accBalance = decimal.Parse(reader.ReadLine()!);

            CreateAccountDto dto = new()
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

            writer.WriteLine("Account created.");
        }

        private async Task HandleDeposit()
        {
            writer.WriteLine(await _controller.ShowAccountsAsync());

            writer.Write("Account Number: ");
            int accNum = int.Parse(reader.ReadLine()!);

            writer.Write("Amount: ");
            decimal amount = decimal.Parse(reader.ReadLine()!);

            TransactionInputDto dto = new()
            {
                AccountNumber = accNum,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto))
            {
                return;
            }

            await _controller.DepositAsync(dto);

            writer.WriteLine("Deposit successful.");
        }

        private async Task HandleWithdraw()
        {
            writer.WriteLine(await _controller.ShowAccountsAsync());

            writer.Write("Account Number: ");
            int accNum = int.Parse(reader.ReadLine()!);

            writer.Write("Amount: ");
            decimal amount = decimal.Parse(reader.ReadLine()!);

            var dto = new TransactionInputDto
            {
                AccountNumber = accNum,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto))
            {
                return;
            }

            await _controller.WithdrawAsync(dto);

            writer.WriteLine("Withdrawal successful.");
        }

        private async Task HandleTransfer()
        {
            writer.WriteLine(await _controller.ShowAccountsAsync());

            writer.Write("From Account Number: ");
            int from = int.Parse(reader.ReadLine()!);

            writer.Write("To Account Number: ");
            int to = int.Parse(reader.ReadLine()!);

            writer.Write("Amount: ");
            decimal amount = decimal.Parse(reader.ReadLine()!);

            TransferInputDto dto = new()
            {
                FromAccountNumber = from,
                ToAccountNumber = to,
                Amount = amount
            };

            if (!DtoValidator.IsValid(dto)) return;

            await _controller.TransferAsync(dto);
            writer.WriteLine("Transfer successful.");
        }

        private async Task HandleShowAccounts()
        {
            string result = await _controller.ShowAccountsAsync();

            writer.WriteLine(result);
        }
    }
}