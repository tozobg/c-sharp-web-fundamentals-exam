using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Models;

namespace TransactionSystem.Core.Services
{
    public class TransactionService
    {
        private readonly IUnitOfWork _uow;

        public TransactionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Deposit money
        public async Task DepositAsync(TransactionInputDto input)
        {
            Account? account = await _uow.Accounts.GetByAccountNumberAsync(input.AccountNumber);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            account.Balance += input.Amount;

            var deposit = new Deposit
            {
                AccountId = account.Id,
                Money = input.Amount
            };

            await _uow.Deposits.AddAsync(deposit);
            await _uow.CompleteAsync();
        }

        // Withdraw money
        public async Task WithdrawAsync(TransactionInputDto input)
        {
            Account? account = await _uow.Accounts.GetByAccountNumberAsync(input.AccountNumber);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            if (account.Balance < input.Amount)
            {
                throw new Exception("Insufficient funds.");
            }

            account.Balance -= input.Amount;

            var withdraw = new Withdraw
            {
                AccountId = account.Id,
                Money = input.Amount
            };

            await _uow.Withdraws.AddAsync(withdraw);
            await _uow.CompleteAsync();
        }

        // Transfer money between accounts
        public async Task TransferAsync(TransferInputDto input)
        {
            Account? from = await _uow.Accounts.GetByAccountNumberAsync(input.FromAccountNumber);
            Account? to = await _uow.Accounts.GetByAccountNumberAsync(input.ToAccountNumber);

            if (from == null || to == null)
            {
                throw new Exception("One or both accounts not found.");
            }

            if (from.Balance < input.Amount)
            {
                throw new Exception("Insufficient funds.");
            }

            from.Balance -= input.Amount;
            to.Balance += input.Amount;

            var transfer = new Transfer
            {
                FromAccountId = from.Id,
                ToAccountId = to.Id,
                Money = input.Amount
            };

            await _uow.Transfers.AddAsync(transfer);
            await _uow.CompleteAsync();
        }
    }
}
