using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_9_s31105.Models;

[Table ("Doctor")]
public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [MaxLength(100)]
    public String FirstName { get; set; }
    [MaxLength(100)]
    public String LastName { get; set; }
    [MaxLength(100)]
    public String Email { get; set; }
    public virtual ICollection<Prescription> Prescription { get; set; } = null!;
}