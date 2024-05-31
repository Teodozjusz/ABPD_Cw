using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia6.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(100)")]
    public string Birthdate { get; set; } = null!;
    
    // Navigation properties
    public Prescription Prescription { get; set; } = null!;
}