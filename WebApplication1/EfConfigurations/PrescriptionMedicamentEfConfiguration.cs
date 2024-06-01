using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
{
    public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
    {
        builder.HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        builder.Property(pm => pm.Details)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne<Medicament>(pm => pm.Medicaments)
            .WithMany(pm => pm.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
        
        builder.HasOne<Prescription>(pm => pm.Prescriptions)
            .WithMany(pm => pm.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdPrescription);

        builder.HasData(new List<Prescription_Medicament>()
        {
            new Prescription_Medicament()
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Dose = 2,
                Details = "Twice a day"
            },
            new Prescription_Medicament()
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Dose = 1,
                Details = "Once a day"
            }
        });
    }
}