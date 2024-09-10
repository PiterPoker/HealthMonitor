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
    internal class PersonEntityTypeConfiguration
        : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons", string.Empty);

            builder.HasKey(cr => cr.Id);

            builder.Property(u => u.Id)
                .UseHiLo("persons_Id_seq", string.Empty);

            builder
                .HasDiscriminator<Guid>("persons_type")
                .HasValue<Person>(Guid.Parse("a23d0fe2-f362-4f9b-b7a4-bc01bafd7940"))
                .HasValue<Patient>(Guid.Parse("597cf4f2-99b6-427a-b3f9-4b3f6eed3b6a"));

            builder
                .Property<int>("_recordTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("record_type_id")
                .IsRequired(false);
            builder
                .Property<string?>("_givenJson")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("record_type_id")
                .IsRequired(false);
            builder
                .Property<int>("_recordTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("record_type_id")
                .IsRequired();

        }
    }
}
