using HealthMonitor.Domain.Exceptions;
using HealthMonitor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class RecordType
        : Enumeration
    {
        public static RecordType Usual = new RecordType(1, "usual".ToLowerInvariant());
        public static RecordType Official = new RecordType(2, "official".ToLowerInvariant());
        public static RecordType Temp = new RecordType(3, "temp ".ToLowerInvariant());
        public static RecordType Secondary = new RecordType(4, "secondary".ToLowerInvariant());
        public static RecordType Old = new RecordType(5, "old".ToLowerInvariant());

        public RecordType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<RecordType> List() =>
            new[] { Usual, Official, Temp, Secondary, Old };

        public static RecordType FromName(string name)
        {
            var recordTypees = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (recordTypees is null)
                throw new HealthMonitorException($"Possible values for recordType: {string.Join(",", List().Select(s => s.Name))}");

            return recordTypees;
        }

        public static RecordType From(int id)
        {
            var recordTypees = List()
                .SingleOrDefault(s => s.Id == id);

            if (recordTypees is null)
                throw new HealthMonitorException($"Possible values for recordType: {string.Join(",", List().Select(s => s.Name))}");

            return recordTypees;
        }
    }
}
