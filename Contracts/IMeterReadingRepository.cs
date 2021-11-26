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
        Task<List<MeterReading>> GetAllMeterReadingsAsync(bool trackChanges);
        Task<MeterReading> GetMeterReadingByIdAsync(DateTime meterReadingDateTime, bool trackChanges);
        void CreateMeterReading(MeterReading meterReading);
        void UpdateMeterReading(MeterReading meterReading);
        void DeleteMeterReading(MeterReading meterReading);
    }
}
