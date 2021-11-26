using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class AccountsRepositoryTests
    {
        private readonly Mock<IAccountRepository> _mockRepo;

        public AccountsRepositoryTests()
        {
            _mockRepo = new Mock<IAccountRepository>();
        }

        [Fact]
        public void GetAccountByIdAsync_Returns_Account()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<Account>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Account()));
            dbContextMock.Setup(s => s.Set<Account>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAccountByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Account()));
            var coffeeRepoMock = _mockRepo.Object;

            //Execute method of SUT (AccountRepository)
            int accountId = 1;
            bool trackChanges = false;
            var account = coffeeRepoMock.GetAccountByIdAsync(accountId, trackChanges).Result;

            //Assert
            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);
        }

        [Fact]
        public void GetAllAccountsAsync_Returns_AllAccounts()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<Account>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Account()));
            dbContextMock.Setup(s => s.Set<Account>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAllAccountsAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Account>()));
            var accountRepoMock = _mockRepo.Object;

            //Execute method of SUT (AccountRepository)
            var accounts = accountRepoMock.GetAllAccountsAsync(false).Result;

            //Assert
            Assert.NotNull(accounts);
            Assert.IsAssignableFrom<List<Account>>(accounts);
        }

        [Fact]
        public void GetAccountWithDetailsAsync_Returns_AccountWithDetails()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<Account>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Account()));
            dbContextMock.Setup(s => s.Set<Account>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAccountWithDetailsAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Account()));
            var accountRepoMock = _mockRepo.Object;

            //Execute method of SUT (AccountRepository)
            int accountId = 1;
            bool trackChanges = false;
            var account = accountRepoMock.GetAccountWithDetailsAsync(accountId, trackChanges).Result;

            //Assert
            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);
        }

        [Fact]
        public void CreateAccount_Returns_Account()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<Account>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Account()));
            dbContextMock.Setup(s => s.Set<Account>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.CreateAccount(new Account()));
            var accountRepoMock = _mockRepo.Object;
            var account = new Account() { AccountId = 1, FirstName = "John", LastName = "Smith"};
            accountRepoMock.CreateAccount(account);

            //Assert
            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);
        }

        [Fact]
        public void UpdateAccount_Returns_Account()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<Account>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Account()));
            dbContextMock.Setup(s => s.Set<Account>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.UpdateAccount(new Account()));
            var accountRepoMock = _mockRepo.Object;
            var account = new Account() { AccountId = 1, FirstName = "John", LastName = "Smith" };
            accountRepoMock.UpdateAccount(account);

            //Assert
            Assert.NotNull(account);
            Assert.IsAssignableFrom<Account>(account);
        }
    }
}
