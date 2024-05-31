using Cwiczenia3.Models;

namespace Cwiczenia3.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAllAnimals(string orderBy);
    
    IEnumerable<Animal> GetAllAnimals();
    int CreateAnimal(Animal animal);
    Animal GetAnimal(int idAnimal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
}