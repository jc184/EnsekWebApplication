using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EnsekDbContext : DbContext
    {
        public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasMany(s => s.MeterReadings);
            modelBuilder.Entity<MeterReading>().HasKey(a => a.MeterReadingDateTime);
            modelBuilder.Entity<Account>().HasIndex(p => new { p.AccountId, p.FirstName, p.LastName }).IsUnique();
        }
    }
}
