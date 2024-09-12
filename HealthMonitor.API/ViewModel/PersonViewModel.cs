using HealthMonitor.Domain.AggregatesModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthMonitor.API.ViewModels
{
    public record PersonViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [Required]
        [JsonPropertyName("family")]
        public string Family { get; set; }
        [Required]
        [JsonPropertyName("use")]
        public string RecordType { get; set; } = Domain.AggregatesModel.RecordType.Official.Name;
        [JsonPropertyName("given")]
        public string[] Given { get; set; }
    }
}
