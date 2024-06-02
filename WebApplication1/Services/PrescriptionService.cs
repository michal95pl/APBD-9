using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public Task<PrescriptionDto> GetPrescription(int idPrescription);
    public Task<bool> PrescriptionExists(int idPrescription);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;
    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> PrescriptionExists(int idPrescription)
    {
        var cnt = await _context.PrescriptionMedicaments.CountAsync(pm => pm.IdPrescription == idPrescription);
        return cnt >= 1;
    }
    
     public async Task<PrescriptionDto> GetPrescription(int idPrescription)
    {
        var medicaments = await _context.PrescriptionMedicaments
            .Where(pm => pm.IdPrescription == idPrescription)
            .Join(
                _context.Medicaments,
                pm => pm.IdMedicament,
                m => m.IdMedicament,
                (pm, m) => new MedicamentDto()
                {
                    Name = m.Name!,
                    Description = m.Description!,
                    Type = m.Type!
                }
            ).ToListAsync();

        var data = await _context.Prescriptions
            .Where(pm => pm.IdPrescription == idPrescription)
            .Join(
                _context.Doctors,
                pm => pm.IdDoctor,
                d => d.IdDoctor,
                (pm, d) => new
                {
                    FirstNameDoctor = d.FirstName,
                    LastNameDoctor = d.LastName,
                    EmailDoctor = d.Email,
                    DatePrescription = pm.Date,
                    DueDatePrescription = pm.DueDate,
                    IdPatient = pm.IdPatient
                }
            )
            .Join(
                _context.Patients,
                pm => pm.IdPatient,
                p => p.IdPatient,
                (pm, p) => new
                {
                    FristNamePatient = p.FirstName,
                    LastNamePatient = p.LastName,
                    BirthDatePatient = p.BirthDate,

                    FirstNameDoctor = pm.FirstNameDoctor,
                    LastNameDoctor = pm.LastNameDoctor,
                    EmailDoctor = pm.EmailDoctor
                }
            ).FirstAsync();


        return new PrescriptionDto()
        {
            FristNamePatient = data.FristNamePatient!,
            LastNamePatient = data.LastNamePatient!,
            BirthDatePatient = data.BirthDatePatient,

            FirstNameDoctor = data.FirstNameDoctor!,
            LastNameDoctor = data.LastNameDoctor!,
            EmailDoctor = data.EmailDoctor!,
            
            Medicaments = medicaments
        };
    }
}