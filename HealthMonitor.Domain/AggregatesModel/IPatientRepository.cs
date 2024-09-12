using HealthMonitor.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitor.Domain.AggregatesModel
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Add(Patient patient);
        Patient Update(Patient patient);
        Patient Create(Patient patient);
        Task<Patient> Get(Guid Id);
        Task<IEnumerable<Patient>> GetAll(Expression<Func<Patient, bool>>? predicate = default);
        Task Delete(Guid Id);
    }
}
