using CW_9_s31105.Data;
using CW_9_s31105.DTOs;
using CW_9_s31105.Exceptions;
using CW_9_s31105.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s31105.Services;

public interface IDbService
{
    public Task CreatePrescriptionAsync(PrescriptionCreateDTO prescription);
    public Task<PatientGetDTO> GetPatientDetailsAsync(int patientId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task CreatePrescriptionAsync(PrescriptionCreateDTO prescription)
    {
        if (prescription.Medicaments is not null && prescription.Medicaments.Count != 0)
        {
            foreach (var Med in prescription.Medicaments)
            {
                var MedId = await data.Medicaments.FirstOrDefaultAsync(g => g.IdMedicament == Med.IdMedicament);
                if (Med is null)
                {
                    throw new NotFoundException($"Medicament with id: {MedId} not found");
                }
            }
        }

        if (prescription.Medicaments.Count > 10)
        {
            throw new TooManyMedicamentsException("Too many medicaments");
        }

        if (prescription.DueDate < prescription.Date)
        {
            throw new PastDueDateException("Past due date");
        }
        
        
        var patientId = prescription.Patient.IdPatient;
        var group = await data.Patients.FirstOrDefaultAsync(g => g.IdPatient == patientId);
        if (group is null)
        {
            var newPatient = new Patient()
            {
                IdPatient = patientId,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
                
            };
            await data.Patients.AddAsync(newPatient);
            await data.SaveChangesAsync();
        }

        var newPrescription = new Prescription()
        {
            //IdPrescription = prescription.Id
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = patientId,
            IdDoctor = prescription.IdDoctor,
        };
        await data.Prescriptions.AddAsync(newPrescription);
        await data.SaveChangesAsync();

        foreach (var med in prescription.Medicaments)
        {
            var prescriptionMedicament = new Prescription_Medicament
            {
                IdMedicament = med.IdMedicament,
                IdPerscription = newPrescription.IdPrescription,
                Dose = med.Dose, 
                Details = med.Description 
            };
            await data.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        }

        await data.SaveChangesAsync();
    }
    
    
    public async Task<PatientGetDTO> GetPatientDetailsAsync(int patientId)
    {
        var patient = await data.Patients
            .Where(p => p.IdPatient == patientId)
            .Select(p => new PatientGetDTO
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionGetDTO
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorGetDTO
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        },
                        Medicaments = pr.Prescription_Medicaments.Select(pm => new MedicamentGetDTO
                        {
                            IdMedicament = pm.medicament.IdMedicament,
                            Name = pm.medicament.Name,
                            Description = pm.medicament.Description,
                            Type = pm.medicament.Type,
                            Dose = pm.Dose,
                            Details = pm.Details
                        }).ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (patient == null)
        {
            throw new NotFoundException($"Patient with id: {patientId} not found");
        }

        return patient;
    }

    
}