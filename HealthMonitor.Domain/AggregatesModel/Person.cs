using HealthMonitor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class Person : Entity, IAggregateRoot
    {
        private int? _recordTypeId;
        private string? _givenJson;
        private string _family;
        public RecordType RecordType { get => RecordType.From(_recordTypeId.Value); set => _recordTypeId = value.Id; }
        public string Family { get => _family; set => _family = value; }
        public string? GivenJson { get => _givenJson; set => _givenJson = value; }
    }
}
