using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PetType : byte
{
    None,
    Cat,
    Rat,
    Owl
}