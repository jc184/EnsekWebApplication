using CsvHelper;
using CsvHelper.Configuration;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnsekWebApplication
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly EnsekDbContext _dbContext;

        public DbInitializer(IServiceScopeFactory scopeFactory, EnsekDbContext ensekDbContext)
        {
            _scopeFactory = scopeFactory;
            _dbContext = ensekDbContext;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EnsekDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        public void SeedData()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName1 = "EnsekWebApplication.Test_Accounts.csv";
            
            using (Stream stream = assembly.GetManifestResourceStream(resourceName1))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvConfiguration config = new(System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"))
                     {
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        IgnoreBlankLines = false
                    };
                    CsvReader csvReader = new CsvReader(reader, config);
                    var records = csvReader.GetRecords<Account>().ToArray();

                    foreach (Account record in records)
                    {
                        var entity = _dbContext.Accounts.Find(record.AccountId); //To Avoid tracking error

                        if (entity != null)
                        {
                            _dbContext.Entry(entity).State = EntityState.Detached;
                            _dbContext.Accounts.AddRange(record);
                        }
                        _dbContext.Accounts.AddRange(record);
                    }


                }
            }
            _dbContext.SaveChanges();
        }

        public int AddMeterReadings()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName2 = "EnsekWebApplication.Resources.Uploads.Meter_Reading.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName2))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvConfiguration config = new(System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"))
                    {
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        IgnoreBlankLines = false
                    };
                    CsvReader csvReader = new CsvReader(reader, config);
                    var records = csvReader.GetRecords<MeterReading>().ToArray();
                  
                    foreach (MeterReading record in records)
                    {
                        var entity = _dbContext.MeterReadings.Find(record.MeterReadingDateTime); //To Avoid tracking error
                        
                        if(entity != null)
                        {
                            _dbContext.Entry(entity).State = EntityState.Detached;
                           
                        }
                        if (record.MeterReadValue != null 
                            && !String.IsNullOrEmpty(record.MeterReadValue) 
                            && record.MeterReadValue.All(char.IsDigit)
                            && record.MeterReadValue.Length.Equals(5))
                        {
                            _dbContext.MeterReadings.Add(record);
                        }
                    }
                }
            }
            _dbContext.SaveChanges();
            return _dbContext.MeterReadings.Count();
            
        }
    }
}
