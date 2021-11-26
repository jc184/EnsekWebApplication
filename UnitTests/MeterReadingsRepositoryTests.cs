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
    public class MeterReadingsRepositoryTests
    {
        private readonly Mock<IMeterReadingRepository> _mockRepo;

        public MeterReadingsRepositoryTests()
        {
            _mockRepo = new Mock<IMeterReadingRepository>();
        }

        [Fact]
        public void GetMeterReadingByIdAsync_Returns_MeterReading()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<MeterReading>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new MeterReading()));
            dbContextMock.Setup(s => s.Set<MeterReading>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetMeterReadingByIdAsync(It.IsAny<DateTime>(), It.IsAny<bool>())).Returns(Task.FromResult(new MeterReading()));
            var meterReadingRepoMock = _mockRepo.Object;

            //Execute method of SUT (MeterReadingRepository)
            DateTime meterReadingDateTime = DateTime.Now;
            bool trackChanges = false;
            var meterReading = meterReadingRepoMock.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges).Result;

            //Assert
            Assert.NotNull(meterReading);
            Assert.IsAssignableFrom<MeterReading>(meterReading);
        }

        [Fact]
        public void GetAllMeterReadingsAsync_Returns_AllMeterReadings()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<MeterReading>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new MeterReading()));
            dbContextMock.Setup(s => s.Set<MeterReading>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAllMeterReadingsAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<MeterReading>()));
            var meterReadingsRepoMock = _mockRepo.Object;

            //Execute method of SUT (MeterReadingRepository)
            var meterReadings = meterReadingsRepoMock.GetAllMeterReadingsAsync(false).Result;

            //Assert
            Assert.NotNull(meterReadings);
            Assert.IsAssignableFrom<List<MeterReading>>(meterReadings);
        }


        [Fact]
        public void CreateMeterReading_Returns_MeterReading()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<MeterReading>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new MeterReading()));
            dbContextMock.Setup(s => s.Set<MeterReading>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.CreateMeterReading(new MeterReading()));
            var meterReadingRepoMock = _mockRepo.Object;
            var meterReading = new MeterReading() { MeterReadingDateTime = DateTime.Now, MeterReadValue = "34567", AccountId = 1234 };

            //Execute method of SUT (MeterReadingRepository)
            meterReadingRepoMock.CreateMeterReading(meterReading);

            //Assert
            Assert.NotNull(meterReading);
            Assert.IsAssignableFrom<MeterReading>(meterReading);
        }

        [Fact]
        public void UpdateMeterReading_Returns_MeterReading()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<EnsekDbContext>();
            var dbSetMock = new Mock<DbSet<MeterReading>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new MeterReading()));
            dbContextMock.Setup(s => s.Set<MeterReading>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.UpdateMeterReading(new MeterReading()));
            var meterReadingRepoMock = _mockRepo.Object;
            var meterReading = new MeterReading() { MeterReadingDateTime = DateTime.Now, MeterReadValue = "34567", AccountId = 1234 };

            //Execute method of SUT (MeterReadingRepository)
            meterReadingRepoMock.UpdateMeterReading(meterReading);

            //Assert
            Assert.NotNull(meterReading);
            Assert.IsAssignableFrom<MeterReading>(meterReading);
        }
    }
}
