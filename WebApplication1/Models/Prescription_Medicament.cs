using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Prescription_Medicament
{
    [ForeignKey(nameof(IdMedicament))]
    public Medicament? Medicaments { get; set; }
    
    [ForeignKey(nameof(IdPrescription))]
    public Prescription? Prescriptions { get; set; }
    
    [Key]
    public int IdMedicament { get; set; }
    
    [Key]
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string? Details { get; set; }
}