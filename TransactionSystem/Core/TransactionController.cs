using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.DTOs.Output;
using TransactionSystem.Core.Services;
using TransactionSystem.Models;

namespace TransactionSystem.Core
{
    public class TransactionController
    {
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;

        public TransactionController(AccountService accountService, TransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task AddAccountAsync(CreateAccountDto dto)
            => await _accountService.CreateAccountAsync(dto);

        public async Task DepositAsync(TransactionInputDto dto)
            => await _transactionService.DepositAsync(dto);

        public async Task WithdrawAsync(TransactionInputDto dto)
            => await _transactionService.WithdrawAsync(dto);

        public async Task TransferAsync(TransferInputDto dto)
            => await _transactionService.TransferAsync(dto);

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
            => await _accountService.GetAllAccountsAsync();

        public async Task<string> ShowAccountsAsync()
        {
            StringBuilder result = new();

            IEnumerable<AccountDto> accounts = await _accountService.GetAllAccountsAsync();

            result.AppendLine("--- Accounts ---");
            foreach (AccountDto account in accounts)
            {
                result.AppendLine($"Account: {account.AccountNumber}, Name: {account.FullName}, Balance: {account.Balance}");
            }

            return result.ToString();
        }
    }
}
