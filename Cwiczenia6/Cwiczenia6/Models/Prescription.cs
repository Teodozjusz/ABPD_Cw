using System.ComponentModel.DataAnnotations;

namespace Cwiczenia6.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    
    // Navigation properties
    public ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; } = null!;

    
}