using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BrewingStatus
{
    Brew,
    Replica,
    Discovery
}