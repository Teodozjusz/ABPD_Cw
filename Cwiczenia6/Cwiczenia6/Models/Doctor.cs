using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia6.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public Prescription Prescription { get; set; } = null!;


}