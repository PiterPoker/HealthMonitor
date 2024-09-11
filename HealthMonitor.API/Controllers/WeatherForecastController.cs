using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Infrastructure;
using HealthMonitor.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        DbContextOptions<HealthMonitorContext> _options;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthMonitorContext>();

            _options = optionsBuilder
                .UseNpgsql("Host=dbPostgres;Port=5432;Database=healthdb;Username=admin;Password=admin")
                .Options;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            using (var db = new HealthMonitorContext(_options)) 
            {
                var patientRepository = new PatientRepository(db);
                var test = patientRepository.GetAll();
            }



                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
