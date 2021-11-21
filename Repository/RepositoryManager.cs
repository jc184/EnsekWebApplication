using Contracts;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private EnsekDbContext _ensekDbContext;
        private IAccountRepository _accountRepository;
        private IMeterReadingRepository _meterReadingRepository;
        public RepositoryManager(EnsekDbContext ensekDbContext)
        {
        _ensekDbContext = ensekDbContext;
        }
        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
                _accountRepository = new AccountRepository(_ensekDbContext);
                return _accountRepository;
            }
        }
        public IMeterReadingRepository MeterReadings
        {
            get
            {
                if (_meterReadingRepository == null)
                _meterReadingRepository = new MeterReadingRepository(_ensekDbContext);
                return _meterReadingRepository;
            }
        }
        public Task SaveAsync() => _ensekDbContext.SaveChangesAsync();
    }
}
