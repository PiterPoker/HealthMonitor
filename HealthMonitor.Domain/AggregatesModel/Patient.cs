using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class Patient : Person
    {
		private int? _genderId;
		private DateTime _birthDate;
		private string? _status;

        public Gender Gender
		{
			get { return Gender.From(_genderId.Value); }
			set { _genderId = _genderId = value.Id; }
		}

		public DateTime BirthDate
        {
			get { return _birthDate; }
			set { _birthDate = value; }
		}

		public Status Status
        {
			get { return Status.FromName(_status); }
			set { _status = value.Name; }
		}


	}
}
