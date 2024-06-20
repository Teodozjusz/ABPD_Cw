using System.Collections;

namespace TravelApp.Models;

public class TripsDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    
    public IList<string> Countries { get; set; }
    
    public IList<ClientDTO> Clients { get; set; }
}