using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia6.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(100)")]
    public string Description { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(100)")]
    public string Type { get; set; } = null!;

    // Navigation properties
    public ICollection<PrescriptionMedicament> PrescriptionMedicament { get; set; } = null!;

}