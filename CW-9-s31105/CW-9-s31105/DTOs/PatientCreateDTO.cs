using System.ComponentModel.DataAnnotations;

namespace CW_9_s31105.DTOs;

public class PatientCreateDTO
{
    public int IdPatient { get; set; }
    [MaxLength(100)]
    [Required]
    public String FirstName { get; set; }
    [MaxLength(100)]
    [Required]
    public String LastName { get; set; }
    [MaxLength(100)]
    [Required]
    public DateTime BirthDate { get; set; }
    
}