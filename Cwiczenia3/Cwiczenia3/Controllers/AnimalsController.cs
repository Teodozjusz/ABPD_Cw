using Cwiczenia3.Models;
using Cwiczenia3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia3.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController: ControllerBase
{
    private readonly IAnimalsRepository _repository = new AnimalsRepository("STR");
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_repository.GetAllAnimals()); 
    }
    
    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy)
    {
        return Ok(_repository.GetAllAnimals(orderBy));
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        _repository.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        _repository.UpdateAnimal(animal);
        return Ok();    
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        _repository.DeleteAnimal(id);
        return NoContent();
    }
}