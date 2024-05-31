namespace Cwiczenia6.Models;

public class RecipeRequest
{
    public Patient Patient { get; set; } = null!;
    public ICollection<Medicament> Medicaments { get; set; } = null!;
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}