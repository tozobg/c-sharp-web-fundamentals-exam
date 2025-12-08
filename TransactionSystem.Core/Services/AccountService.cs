using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.DTOs.Output;
using TransactionSystem.Models;

namespace TransactionSystem.Core.Services
{
    public class AccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Create a new account
        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto input)
        {
            // Check if account number exists
            Account? existing = await _uow.Accounts.GetByAccountNumberAsync(input.AccountNumber);

            if (existing != null)
            {
                throw new Exception("Account number already exists.");
            }

            Account account = new ()
            {
                AccountNumber = input.AccountNumber,
                FullName = input.FullName,
                Balance = input.Balance
            };

            await _uow.Accounts.AddAsync(account);
            await _uow.CompleteAsync();

            return new AccountDto
            {
                AccountNumber = account.AccountNumber,
                FullName = account.FullName,
                Balance = account.Balance
            };
        }

        // Get all accounts for display
        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _uow.Accounts.GetAllAsync();
            return accounts.Select(a => new AccountDto
            {
                AccountNumber = a.AccountNumber,
                FullName = a.FullName,
                Balance = a.Balance
            });
        }
    }
}
