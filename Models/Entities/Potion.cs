using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities;

[System.Serializable]
public class Potion
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    public Student Student { get; set; }
    
    public List<Ingredients> Ingredients { get; set; }
    [JsonIgnore]
    public BrewingStatus Status { get; set; } = BrewingStatus.Brew;
    [JsonIgnore]
    public Recipe Recipe { get; set; }
    
    
    
    

    
}