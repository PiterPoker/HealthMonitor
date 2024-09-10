using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.Exceptions
{
    public class HealthMonitorException : Exception
    {
        public HealthMonitorException()
        { }

        public HealthMonitorException(string message)
            : base(message)
        { }

        public HealthMonitorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
