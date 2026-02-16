using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionSystem.Core.DTOs.Input;
using TransactionSystem.Core.Services;

namespace TransactionSystem.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _service = null!;

        [SetUp]
        public void Setup()
        {
            _uow = new InMemoryUnitOfWork();
            _service = new AccountService(_uow);
        }

        [TearDown]
        public void Cleanup()
        {
            _uow.Dispose();
        }

        [Test]
        public async Task CreateAccount_Should_Add_Account()
        {
            var input = new CreateAccountDto
            {
                AccountNumber = 1,
                FullName = "Anatoli Ivanov",
                Balance = 200
            };

            var result = await _service.CreateAccountAsync(input);

            Assert.That(result.AccountNumber, Is.EqualTo(input.AccountNumber));
            Assert.That(result.FullName, Is.EqualTo(input.FullName));
            Assert.That(result.Balance, Is.EqualTo(input.Balance));

            var allAccounts = await _service.GetAllAccountsAsync();
            Assert.That(allAccounts.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateAccount_Should_Throw_When_AccountNumberExists()
        {
            var input = new CreateAccountDto
            {
                AccountNumber = 1,
                FullName = "Petyr Petrov",
                Balance = 200
            };

            await _service.CreateAccountAsync(input);

            var duplicate = new CreateAccountDto
            {
                AccountNumber = 1,
                FullName = "Petyr Ivanov",
                Balance = 300
            };

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _service.CreateAccountAsync(duplicate);
            });
        }

        [Test]
        public async Task GetAllAccounts_ShouldReturnMultipleAccounts()
        {
            await _service.CreateAccountAsync(new CreateAccountDto
            {
                AccountNumber = 1,
                FullName = "Petyr Petrov",
                Balance = 100
            });
            await _service.CreateAccountAsync(new CreateAccountDto
            {
                AccountNumber = 2,
                FullName = "Petyr Ivanov",
                Balance = 200
            });

            var all = await _service.GetAllAccountsAsync();
            Assert.That(all.Count(), Is.EqualTo(2));
            Assert.IsTrue(all.Any(a => a.FullName == "Petyr Petrov"));
            Assert.IsTrue(all.Any(a => a.FullName == "Petyr Ivanov"));
        }
    }
}
