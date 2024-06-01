using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(p => p.IdPrescription);
        builder.Property(p => p.IdPrescription).ValueGeneratedOnAdd();

        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.Date).IsRequired();

        builder.HasOne<Patient>(p => p.Patients)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
        
        builder.HasOne<Doctor>(p => p.Doctors)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);

        builder.HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 1,
                Date = new DateOnly(2021, 1, 1),
                DueDate = new DateOnly(2021, 1, 15),
                IdPatient = 1,
                IdDoctor = 1
            }
        });
    }
}