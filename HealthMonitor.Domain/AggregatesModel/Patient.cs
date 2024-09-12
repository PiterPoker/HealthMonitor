using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public class Patient : Person
    {
		private int _genderId;
		private DateTime _birthDate;
		private string? _status;

        public int GenderId
		{
			get { return _genderId; }
			set { _genderId = _genderId = value; }
		}

		public DateTime BirthDate
        {
			get { return _birthDate; }
			set { _birthDate = value; }
		}

		public string? StatusId
        {
			get { return _status; }
			set { _status = value; }
		}


	}
}
