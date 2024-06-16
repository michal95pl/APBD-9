using Microsoft.EntityFrameworkCore;
using WebApplication1.EfConfigurations;
using WebApplication1.Models;

namespace WebApplication1.Context;

public class AppDbContext : DbContext
{
    private IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEfConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserEfConfiguraion());
    }
    
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    
    public DbSet<User> Users { get; set; }
}