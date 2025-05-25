namespace CW_9_s31105.DTOs;

public class PrescriptionGetDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDTO Doctor { get; set; } = null!;
    public List<MedicamentGetDTO> Medicaments { get; set; } = [];
}