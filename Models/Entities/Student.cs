using System;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities;

[Serializable]
public class Student
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Name { get; set; }

    public HouseType HouseType { get; set; }

    public PetType PetType { get; set; }

    public Room Room { get; set; }
}