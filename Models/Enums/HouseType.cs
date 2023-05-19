using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HouseType : byte
    {
        Gryffindor,
        Hufflepuff,
        Ravenclaw,
        Slytherin
    }
}
