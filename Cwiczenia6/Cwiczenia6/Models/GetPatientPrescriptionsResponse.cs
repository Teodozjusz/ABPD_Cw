namespace Cwiczenia6.Models;

public class GetPatientPrescriptionsResponse
{
    public int IdPatient { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}