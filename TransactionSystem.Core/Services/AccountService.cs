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
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                FullName = a.FullName,
                Balance = a.Balance
            });
        }

        // Get a single account by its Database ID
        public async Task<AccountDto?> GetByIdAsync(int id)
        {
            var account = await _uow.Accounts.GetByIdAsync(id);

            if (account == null) return null;

            return new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                FullName = account.FullName,
                Balance = account.Balance
            };
        }

        // Update allowed properties (Name and AccountNumber only)
        public async Task UpdateAccountAsync(UpdateAccountDto input)
        {
            var account = await _uow.Accounts.GetByIdAsync(input.Id);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            // Check if the NEW AccountNumber is already taken by SOMEONE ELSE
            if (account.AccountNumber != input.AccountNumber)
            {
                var duplicate = await _uow.Accounts.GetByAccountNumberAsync(input.AccountNumber);
                if (duplicate != null)
                {
                    throw new Exception("The new Account Number is already in use by another account.");
                }
            }

            account.FullName = input.FullName;
            account.AccountNumber = input.AccountNumber;

            await _uow.CompleteAsync();
        }
    }
}
