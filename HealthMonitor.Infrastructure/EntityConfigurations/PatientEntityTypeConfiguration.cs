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
                .HasIndex("birthdate");

            builder
                .Property<int>("_genderId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("genderid")
                .IsRequired(false);
            builder
                .Property<DateTime>("_birthDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("birthdate")
                .IsRequired();
            builder
                .Property<string>("_status")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("status")
                .IsRequired(false);

        }
    }
}
