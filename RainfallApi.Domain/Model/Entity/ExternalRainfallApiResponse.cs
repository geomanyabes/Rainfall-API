using Newtonsoft.Json;

namespace RainfallApi.Domain.Model.Entity
{
    public class ExternalRainfallApiResponse
    {
        [JsonProperty("@context")]
        public string? Context { get; set; }
        public MetaData? Meta { get; set; }
        public List<RainfallData>? Items { get; set; }
    }

    public class MetaData
    {
        public string? Publisher { get; set; }
        public string? Licence { get; set; }
        public string? Documentation { get; set; }
        public string? Version { get; set; }
        public string? Comment { get; set; }
        public List<string>? HasFormat { get; set; }
        public int? Limit { get; set; }
    }

    public class RainfallData
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }
        public string? Label { get; set; }
        public LatestReading? LatestReading { get; set; }
        public string? Notation { get; set; }
        public string? Parameter { get; set; }
        public string? ParameterName { get; set; }
        public int? Period { get; set; }
        public string? Qualifier { get; set; }
        public string? Station { get; set; }
        public string? StationReference { get; set; }
        public string? Unit { get; set; }
        public string? UnitName { get; set; }
        public string? ValueType { get; set; }
    }

    public class LatestReading
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }
        public string? Date { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Measure { get; set; }
        public decimal Value { get; set; }
    }
}
