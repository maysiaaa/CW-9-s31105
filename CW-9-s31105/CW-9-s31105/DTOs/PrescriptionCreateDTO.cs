using System.ComponentModel.DataAnnotations;

namespace CW_9_s31105.DTOs;

public class PrescriptionCreateDTO
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    //public int IdPatient { get; set; }
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public PatientCreateDTO Patient { get; set; }
    [Required]
    public virtual ICollection<MedicamentCreateDTO> Medicaments { get; set; } = null!;
}