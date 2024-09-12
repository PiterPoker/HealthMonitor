using HealthMonitor.API.ViewModels;
using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Domain.Exceptions;
using System.Text;
using System.Text.Json;

namespace HealthMonitor.API.Helpers
{
    public static class ViewModelHelper
    {
        public static PatientViewModel ConvertToPatientViewModel(Patient patient)
        {
            return new PatientViewModel
            {
                Gender = Gender.From(patient.GenderId).Name,
                BirthDate = patient.BirthDate,
                Person = new PersonViewModel
                {
                    Id = patient.Id,
                    Family = patient.Family,
                    Given = JsonSerializer.Deserialize<string[]>(patient.GivenJson),
                    RecordType = RecordType.From(patient.RecordTypeId).Name
                },
                Status = Status.FromName(patient.StatusId).Name
            };
        }
        public static Patient ConvertToPatient(PatientViewModel patient)
        {
            return new Patient
            {
                BirthDate = patient.BirthDate >= DateTime.Now.AddYears(-18) ? patient.BirthDate : throw new HealthMonitorException("The patient must not be older than 18 years old."),
                Family = patient.Person.Family,
                GenderId = Gender.FromName(patient.Gender).Id,
                RecordTypeId = RecordType.FromName(patient.Person.RecordType).Id,
                StatusId = Status.FromName(patient.Status).Name,
                GivenJson = JsonSerializer.Serialize(
                    patient.Person.Given,
                    new JsonSerializerOptions
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    }),
            };
        }

        public static Patient Update(Patient dbPatient, Patient updatePatient)
        {
            if (dbPatient.StatusId != updatePatient.StatusId)
                dbPatient.StatusId = updatePatient.StatusId;
            if (dbPatient.BirthDate != updatePatient.BirthDate)
                dbPatient.BirthDate = updatePatient.BirthDate;
            if (dbPatient.GenderId != updatePatient.GenderId)
                dbPatient.GenderId = updatePatient.GenderId;
            if (dbPatient.Family != updatePatient.Family)
                dbPatient.Family = updatePatient.Family;
            if (dbPatient.GivenJson != updatePatient.GivenJson)
                dbPatient.GivenJson = updatePatient.GivenJson;
            if (dbPatient.RecordTypeId != updatePatient.RecordTypeId)
                dbPatient.RecordTypeId = updatePatient.RecordTypeId;
            return dbPatient;
        }
    }
}
