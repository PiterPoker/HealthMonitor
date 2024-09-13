using HealthMonitor.API.ViewModels;
using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Domain.Exceptions;
using HealthMonitor.Infrastructure.Repositories;
using Microsoft.AspNetCore.Server.IIS.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthMonitor.API.Helpers
{
    public static class SearchHelper
    {
        public const string Pattern = @"(ge|le|gt|lt|eq|ne)(\d{4}-\d{2}-\d{2}(?:((T\d{2}| \d{2}):\d{2}))?)";
        public static IQueryable<Patient> SearchPatientByBirthDate(string operatorQuery, DateTime searchDate, IQueryable<Patient> query) 
        {
            return operatorQuery switch
            {
                "gt" => query.Where(p => p.BirthDate > searchDate),
                "lt" => query.Where(p => p.BirthDate < searchDate),
                "ge" => query.Where(p => p.BirthDate >= searchDate),
                "le" => query.Where(p => p.BirthDate <= searchDate),
                "eq" => query.Where(p => p.BirthDate.Date == searchDate.Date),
                "ne" => query.Where(p => p.BirthDate.Date != searchDate.Date),
                _ => throw new HealthMonitorException("Invalid operator. Use one of the following: gt, lt, ge, le, eq, ne."),
            };
        }
    }
}
