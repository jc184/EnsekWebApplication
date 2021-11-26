using Entities;

namespace EnsekWebApplication
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
        string AddMeterReadings();
    }
}
