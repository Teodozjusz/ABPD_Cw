using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia6.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    public string Details { get; set; } = null!;

    
    // Navigation properties
    public Medicament Medicaments { get; set; } = null!;
    public Prescription Prescriptions { get; set; } = null!;
}