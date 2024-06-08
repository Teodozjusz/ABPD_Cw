using Cwiczenia6.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia6.Data;

public class ClinicContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>().HasKey(pm=> new {pm.IdMedicament, pm.IdPrescription});
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=apbd;User Id=sa;Password=Q2w3e4r5;TrustServerCertificate=True");
    }
    
}