using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.DTOs;

public class CreateAnimalDto
{
    [MaxLength(100), Required]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100), Required]
    public string Species { get; set; } = string.Empty;
    [Required]
    public int Age { get; set; }
    [Required]
    public double Weight { get; set; }
}