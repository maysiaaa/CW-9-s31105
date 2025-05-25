using CW_9_s31105.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s31105.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var doctors = new List<Doctor>
        {
            new ()
            {
                IdDoctor = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
            }
        };
        
        var medicaments = new List<Medicament>
        {
            new ()
            {
                IdMedicament = 1,
                Name = "Apap",
                Description = "Przeciwbolowy",
                Type = "Tabletki"
            }
        };
        
        var patients = new List<Patient>
        {
            new ()
            {
                IdPatient = 1,
                FirstName = "Pola",
                LastName = "Gorska",
                BirthDate = DateTime.Now,
            }
        };
        
        var prescriptions = new List<Prescription>
        {
            new ()
            {
                IdPrescription = 1,
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                IdPatient = 1,
                IdDoctor = 1,
            }
        };
        
        var prescriptionMedicaments = new List<Prescription_Medicament>
        {
            new ()
            {
                IdMedicament = 1,
                IdPerscription = 1,
                Dose = 4,
                Details = "Przeciwbolowy",
            }
        };
        
        
    }
    
}