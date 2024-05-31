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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"ConnectionString here");
    }
    
}