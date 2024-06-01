using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Prescription
{
    public int? IdPrescription { get; set; }
    
    public DateOnly? Date { get; set; }
    
    public DateOnly DueDate { get; set; }
    
    public virtual Patient? Patients { get; set; }
    
    public int IdPatient { get; set; }
    
    public virtual Doctor? Doctors { get; set; }
    
    public int IdDoctor { get; set; }
    
    public ICollection<Prescription_Medicament>? Prescription_Medicaments { get; set; }
}