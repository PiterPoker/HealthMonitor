using HealthMonitor.Domain.Exceptions;
using HealthMonitor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class Status
        : Enumeration
    {
        public static Status Active = new Status(1, "Active".ToLowerInvariant());
        public static Status Close = new Status(2, "Close".ToLowerInvariant());

        public Status(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Status> List() =>
            new[] { Active, Close };

        public static Status FromName(string name)
        {
            var statuses = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (statuses is null)
                throw new HealthMonitorException($"Possible values for status: {string.Join(",", List().Select(s => s.Name))}");

            return statuses;
        }

        public static Status From(int id)
        {
            var statuses = List()
                .SingleOrDefault(s => s.Id == id);

            if (statuses is null)
                throw new HealthMonitorException($"Possible values for status: {string.Join(",", List().Select(s => s.Name))}");

            return statuses;
        }
    }
}
