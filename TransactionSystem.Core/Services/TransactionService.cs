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
    public class TransactionService
    {
        private readonly IUnitOfWork _uow;

        public TransactionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Deposits
        public async Task<IEnumerable<TransactionDetailsDto>> GetDepositsByAccountIdAsync(int accountId)
        {
            // Get all deposits from the generic repository
            // TODO: Implement custom extension repository to get all by accountId !
            var allDeposits = await _uow.Deposits.GetAllAsync();

            return allDeposits
                .Where(d => d.AccountId == accountId)
                .Select(d => new TransactionDetailsDto
                {
                    Id = d.Id,
                    AccountId = d.AccountId,
                    Money = d.Money,
                    Date = d.Date,
                    Type = "Deposit"
                })
                .OrderByDescending(d => d.Date); 
        }

        public async Task CreateDepositAsync(TransactionInputDto input)
        {
            Account? account = await _uow.Accounts.GetByIdAsync(input.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            account.Balance += input.Money;

            Deposit deposit = new()
            {
                AccountId = account.Id,
                Money = input.Money
            };

            await _uow.Deposits.AddAsync(deposit);
            await _uow.CompleteAsync();
        }

        public async Task DeleteDepositAsync(int depositId, int accountId)
        {
            Deposit? deposit = await _uow.Deposits.GetByIdAsync(depositId);
            Account? account = await _uow.Accounts.GetByIdAsync(accountId);

            if (deposit != null && account != null)
            {
                // Subtract the money from the account balance
                account.Balance -= deposit.Money;

                // Delete the record
                await _uow.Deposits.RemoveAsync(deposit);

                await _uow.CompleteAsync();
            }
        }
        #endregion

        #region Withdraws
        public async Task<IEnumerable<TransactionDetailsDto>> GetWithdrawalsByAccountIdAsync(int accountId)
        {
            // Get all withdrawals from the generic repository
            // TODO: Implement custom extension repository to get all by accountId !
            var allWithdraws = await _uow.Withdraws.GetAllAsync();

            return allWithdraws
                .Where(d => d.AccountId == accountId)
                .Select(d => new TransactionDetailsDto
                {
                    Id = d.Id,
                    AccountId = d.AccountId,
                    Money = d.Money,
                    Date = d.Date,
                    Type = "Withdraw"
                })
                .OrderByDescending(d => d.Date);
        }

        public async Task CreateWithdrawalAsync(TransactionInputDto input)
        {
            Account? account = await _uow.Accounts.GetByIdAsync(input.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            if (account.Balance < input.Money)
            {
                throw new Exception("Insufficient funds.");
            }

            account.Balance -= input.Money;

            var withdraw = new Withdraw
            {
                AccountId = account.Id,
                Money = input.Money
            };

            await _uow.Withdraws.AddAsync(withdraw);
            await _uow.CompleteAsync();
        }

        public async Task DeleteWithdrawalAsync(int withdrawalId, int accountId)
        {
            Withdraw? withdrawal = await _uow.Withdraws.GetByIdAsync(withdrawalId);
            Account? account = await _uow.Accounts.GetByIdAsync(accountId);

            if (withdrawal != null && account != null)
            {
                account.Balance += withdrawal.Money;

                await _uow.Withdraws.RemoveAsync(withdrawal);

                await _uow.CompleteAsync();
            }
        }
        #endregion

        #region Transfers
        // Transfer money between accounts
        public async Task TransferAsync(TransferInputDto input)
        {
            Account? from = await _uow.Accounts.GetByAccountNumberAsync(input.FromAccountNumber);
            Account? to = await _uow.Accounts.GetByAccountNumberAsync(input.ToAccountNumber);

            if (from == null || to == null)
            {
                throw new Exception("One or both accounts not found.");
            }

            if (from.Balance < input.Money)
            {
                throw new Exception("Insufficient funds.");
            }

            from.Balance -= input.Money;
            to.Balance += input.Money;

            var transfer = new Transfer
            {
                FromAccountId = from.Id,
                ToAccountId = to.Id,
                Money = input.Money
            };

            await _uow.Transfers.AddAsync(transfer);
            await _uow.CompleteAsync();
        }
        #endregion
    }
}
