using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Domain.SeedWork;
using HealthMonitor.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Infrastructure
{
    public class HealthMonitorContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "hm";
        public DbSet<Patient> Patients { get; set; }

        public HealthMonitorContext(DbContextOptions<HealthMonitorContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return false;
        }
    }
}
