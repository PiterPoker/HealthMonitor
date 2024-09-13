using HealthMonitor.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Infrastructure.EntityConfigurations
{
    internal class PatientEntityTypeConfiguration
        : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder
                .Property(p=>p.GenderId)
                .HasColumnName("genderid")
                .IsRequired();
            builder
                .Property<DateTime>(p=>p.BirthDate)
                .HasColumnName("birthdate")
                .IsRequired();
            builder
                .Property(p=>p.StatusId)
                .HasColumnName("status")
                .IsRequired(false);

            builder
                .HasIndex(p=>p.BirthDate);
        }
    }
}
