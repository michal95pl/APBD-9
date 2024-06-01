using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.IdPatient);
        builder.Property(p => p.IdPatient).ValueGeneratedOnAdd();
        
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.BirthDate).IsRequired();

        builder.HasData(new List<Patient>()
        {
            new Patient()
            {
                IdPatient = 1,
                FirstName = "Piotr",
                LastName = "Kowalczyk",
                BirthDate = new DateOnly(2000, 3, 3),
            },
            new Patient()
            {
                IdPatient = 2,
                FirstName = "Karolina",
                LastName = "Kowalska",
                BirthDate = new DateOnly(2005, 4, 4),
            }
        });
    }
}