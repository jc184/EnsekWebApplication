using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(EnsekDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

        public async Task<List<Account>> GetAllAccountsAsync(bool trackChanges) =>
               await FindAll(trackChanges)
                .OrderBy(c => c.AccountId)
                .ToListAsync();


        public async Task<Account> GetAccountByIdAsync(int accountId, bool
            trackChanges) =>
            await FindByCondition(account => account.AccountId.Equals(accountId), trackChanges)
            .SingleOrDefaultAsync();


        public async Task<Account> GetAccountWithDetailsAsync(int accountId, bool trackChanges) =>

            await FindByCondition(account => account.AccountId.Equals(accountId), trackChanges)
                .Include(meterReading => meterReading.MeterReadings)
                .FirstOrDefaultAsync();


        public void UpdateAccount(Account account)
        {
            Update(account);
        }

    }
}
