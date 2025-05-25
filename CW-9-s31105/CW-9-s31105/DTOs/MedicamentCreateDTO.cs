using System.ComponentModel.DataAnnotations;

namespace CW_9_s31105.DTOs;

public class MedicamentCreateDTO
{
    [MaxLength(100)]
    [Required]
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    [Required]
    public String Name { get; set; }
    [MaxLength(100)]
    [Required]
    public String Description { get; set; }
    [MaxLength(100)]
    [Required]
    public String Type { get; set; }
    public int Dose { get; set; }
}