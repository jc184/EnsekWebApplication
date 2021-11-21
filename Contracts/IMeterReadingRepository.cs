using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMeterReadingRepository : IRepositoryBase<MeterReading>
    {
        Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync(bool trackChanges);
        Task<MeterReading> GetMeterReadingByIdAsync(int meterReadingId, bool trackChanges);
        void CreateMeterReading(int accountId, MeterReading meterReading);
        void UpdateMeterReading(MeterReading meterReading);
        void DeleteMeterReading(MeterReading meterReading);
    }
}
