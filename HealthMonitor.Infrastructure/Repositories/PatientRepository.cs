using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthMonitor.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HealthMonitorContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PatientRepository(HealthMonitorContext context)
        {
            _context = context;
        }

        public Patient Add(Patient patient)
        {
            if (patient.IsTransient())
            {
                return _context.Patients
                    .Add(patient)
                    .Entity;
            }
            else
            {
                return patient;
            }
        }

        public async Task Delete(Guid Id)
        {
            var patient = await _context.Patients
                .SingleAsync(c => c.Id == Id);

            _context.Patients.Remove(patient);
        }

        public async Task<Patient> Get(Guid Id)
        {
            var patient = await _context.Patients
                .SingleAsync(c => c.Id == Id);

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAll(Expression<Func<Patient, bool>> predicate)
        {
            return await (predicate is not null ? _context.Patients
                .Where(predicate) :
                _context.Patients)
                .ToListAsync();
        }

        public Patient Update(Patient patient)
        {
            return _context.Patients
                    .Update(patient)
                    .Entity;
        }
    }
}
