using HealthMonitor.API.Helpers.Attributes;
using HealthMonitor.Domain.AggregatesModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthMonitor.API.ViewModels
{
    public record PatientViewModel
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = Domain.AggregatesModel.Gender.Unknown.Name;
        [DateOfBirth]
        [Required(ErrorMessage = "Property BirthDate is required")]
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("active")]
        public string? Status { get; set; }
        [DisplayName("name")]
        [JsonPropertyName("name")]
        public PersonViewModel Person { get; set; }

    }
}
