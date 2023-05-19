using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Entities;

public class Ingredients
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public List<Recipe> Recipes { get; set; }
    [JsonIgnore]
    public List<Potion> Potions { get; set; }
}