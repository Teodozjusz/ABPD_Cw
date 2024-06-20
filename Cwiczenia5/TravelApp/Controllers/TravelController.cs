using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Context;
using TravelApp.Models;

namespace TravelApp.Controllers;

[Route("api/trips")]
[Controller]
public class TravelController : Controller
{
    private ApbdContext _context = new ApbdContext();
    
    [HttpGet]
    public IActionResult GetTrips()
    {
        var response = new List<TripsDTO>();
        
        var trips = _context.Trips
            .Include(trip => trip.IdCountries)
            .Include(trip => trip.ClientTrips).ToList();

        foreach (var trip in trips)
        {
            var countries = trip.IdCountries.Select(country => country.Name).ToList();

            var clientsIds = trip.ClientTrips.Select(clientTrip => clientTrip.IdClient).ToList();

            var clients = _context.Clients
                .Where(client => clientsIds.Contains(client.IdClient))
                .Select(client => new ClientDTO()
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName
                }).ToList();

            var tripDto = new TripsDTO
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,

                Countries = countries,
                Clients = clients

            };

            response.Add(tripDto);
        }

        return Ok(response);
    }

    
    
    [HttpDelete("/{idClient}")]
    public IActionResult DeleteClient(int idClient)
    {
        var client = _context.Clients
            .First(client1 => client1.IdClient == idClient);
        if (ClientHasTrips(client))
            return BadRequest("Client has assigned trips");

        _context.Remove(client);
        _context.SaveChanges();
        
        return Ok();
    }

    private bool ClientHasTrips(Client client)
    {
        var clientTrips = _context.ClientTrips
            .Where(trip => trip.IdClient == client.IdClient).ToList();
        return clientTrips.Count > 0;
    }

    [HttpPost("/{idClient}/clients")]
    public IActionResult AssignClientToTrip(AssignClientDto request, int idClient)
    {
        Client client = EnsureClientExists(request);

        var clientTrips = _context.ClientTrips.ToList();
        
        if (IsClientAlreadyAssignedToTrip(request, clientTrips))
            return BadRequest("Client is already assigned to trip");

        if ( !_context.Trips.Any(trip => trip.IdTrip == request.IdTrip) )
            return BadRequest("Trip with given ID doesn't exist!");

        var newClientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = request.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = request.PaymentDate
        };

        _context.ClientTrips.Add(newClientTrip);
        _context.SaveChanges();
        
        return Ok();
    }

    
    private Client EnsureClientExists(AssignClientDto request)
    {
        var clientsMatchingPesel = _context.Clients.Where(client => client.Pesel == request.Pesel).ToList();
        Client client;
        if (clientsMatchingPesel.Count == 0)
        {
            client = new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Pesel = request.Pesel,
                Telephone = request.Telephone,
                Email = request.Email
            };
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
        else
        {
            client = clientsMatchingPesel.First();
        }

        return client;
    }
    
    private bool IsClientAlreadyAssignedToTrip(AssignClientDto request, List<ClientTrip> clientTrips)
    {
        var client = _context.Clients.First(c => c.Pesel == request.Pesel);
        var currentClientTrips = clientTrips
            .Where(trip => trip.IdClient == client.IdClient)
            .Where(trip => trip.IdTrip == request.IdTrip).ToList();
        return currentClientTrips.Count > 0;

    }
}