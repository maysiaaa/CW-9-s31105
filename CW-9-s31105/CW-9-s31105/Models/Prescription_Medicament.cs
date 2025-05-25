using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s31105.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdPerscription), nameof(IdMedicament))]
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }
    public int IdPerscription { get; set; }
    public int Dose { get; set; }
    [MaxLength(100)]
    public String Details { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament medicament { get; set; } = null!;
    [ForeignKey(nameof(IdPerscription))]
    public virtual Prescription prescription { get; set; } = null!;
}