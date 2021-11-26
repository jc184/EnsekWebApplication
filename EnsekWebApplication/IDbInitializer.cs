using Entities;

namespace EnsekWebApplication
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
        int AddMeterReadings();
        int GetMeterReadingsCount();
    }
}
