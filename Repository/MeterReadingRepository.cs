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
    public class MeterReadingRepository : RepositoryBase<MeterReading>, IMeterReadingRepository
    {
        public MeterReadingRepository(EnsekDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateMeterReading(MeterReading meterReading)
        {
            Create(meterReading);
        }

        public void DeleteMeterReading(MeterReading meterReading)
        {
            Delete(meterReading);
        }

        public async Task<List<MeterReading>> GetAllMeterReadingsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(mr => mr.AccountId)
                .ToListAsync();


        public async Task<MeterReading> GetMeterReadingByIdAsync(DateTime meterReadingDateTime, bool
            trackChanges) =>
            await FindByCondition(meterReading => meterReading.MeterReadingDateTime.Equals(meterReadingDateTime), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateMeterReading(MeterReading meterReading)
        {
            Update(meterReading);
        }
    }
}
