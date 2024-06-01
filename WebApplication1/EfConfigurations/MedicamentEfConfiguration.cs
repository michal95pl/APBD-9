using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicaments");

        builder.HasKey(m => m.IdMedicament);
        builder.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
        
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Description).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Type).IsRequired().HasMaxLength(100);

        builder.HasData(new List<Medicament>()
        {
            new Medicament()
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Painkiller",
                Type = "Painkiller"
            },
            new Medicament()
            {
                IdMedicament = 2,
                Name = "Ibuprofen",
                Description = "Painkiller",
                Type = "Painkiller"
            }
        });
    }
}