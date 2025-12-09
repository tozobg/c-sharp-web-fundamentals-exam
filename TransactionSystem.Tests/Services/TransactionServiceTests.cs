using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.Services;
using TransactionSystem.Data.InMemory.UnitOfWork;

namespace TransactionSystem.Tests.Services
{
    public class TransactionServiceTests
    {
        private InMemoryUnitOfWork _uow = null!;
        private AccountService _accountService = null!;
        private TransactionService _transactionService = null!;

        [SetUp]
        public async Task Setup()
        {
            _uow = new InMemoryUnitOfWork();
            _accountService = new AccountService(_uow);
            _transactionService = new TransactionService(_uow);

            // Add two accounts for tests
            await _accountService.CreateAccountAsync(new CreateAccountDto
            {
                AccountNumber = 1,
                FullName = "Petyr Petrov",
                Balance = 1000
            });

            await _accountService.CreateAccountAsync(new CreateAccountDto
            {
                AccountNumber = 2,
                FullName = "Ivan Ivanov",
                Balance = 500
            });
        }

        [TearDown]
        public void Cleanup()
        {
            _uow.Dispose();
        }

        [Test]
        public async Task Deposit_Should_Increase_Balance()
        {
            await _transactionService.DepositAsync(new TransactionInputDto
            {
                AccountNumber = 1,
                Amount = 200
            });

            var account = await _uow.Accounts.GetByAccountNumberAsync(1);
            Assert.That(account!.Balance, Is.EqualTo(1200));
        }

        [Test]
        public async Task Withdraw_Should_Decrease_Balance()
        {
            await _transactionService.WithdrawAsync(new TransactionInputDto
            {
                AccountNumber = 1,
                Amount = 300
            });

            var account = await _uow.Accounts.GetByAccountNumberAsync(1);
            Assert.That(account!.Balance, Is.EqualTo(700));
        }

        [Test]
        public void Withdraw_Should_Throw_When_InsufficientFunds()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _transactionService.WithdrawAsync(new TransactionInputDto
                {
                    AccountNumber = 2,
                    Amount = 1000
                });
            });
        }

        [Test]
        public async Task Transfer_Should_Move_Money_Between_Accounts()
        {
            await _transactionService.TransferAsync(new TransferInputDto
            {
                FromAccountNumber = 1,
                ToAccountNumber = 2,
                Amount = 400
            });

            var from = await _uow.Accounts.GetByAccountNumberAsync(1);
            var to = await _uow.Accounts.GetByAccountNumberAsync(2);

            Assert.That(from!.Balance, Is.EqualTo(600));
            Assert.That(to!.Balance, Is.EqualTo(900));
        }

        [Test]
        public void Transfer_Should_Throw_When_InsufficientFunds()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _transactionService.TransferAsync(new TransferInputDto
                {
                    FromAccountNumber = 2,
                    ToAccountNumber = 1,
                    Amount = 1000
                });
            });
        }
    }
}
