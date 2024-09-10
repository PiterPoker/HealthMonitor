using HealthMonitor.Domain.Exceptions;
using HealthMonitor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class Gender
        : Enumeration
    {
        public static Gender Male = new Gender(1, "male".ToLowerInvariant());
        public static Gender Female = new Gender(2, "female".ToLowerInvariant());
        public static Gender Other = new Gender(3, "other".ToLowerInvariant());
        public static Gender Unknown = new Gender(4, "unknown".ToLowerInvariant());

        public Gender(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Gender> List() =>
            new[] { Male, Female, Other, Unknown };

        public static Gender FromName(string name)
        {
            var genderes = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (genderes is null)
                throw new HealthMonitorException($"Possible values for gender: {string.Join(",", List().Select(s => s.Name))}");

            return genderes;
        }

        public static Gender From(int id)
        {
            var genderes = List()
                .SingleOrDefault(s => s.Id == id);

            if (genderes is null)
                throw new HealthMonitorException($"Possible values for gender: {string.Join(",", List().Select(s => s.Name))}");

            return genderes;
        }
    }
}
