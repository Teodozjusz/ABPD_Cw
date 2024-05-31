using System.Data.SqlClient;
using Cwiczenia3.Models;

namespace Cwiczenia3.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private string connectionString;
    
    public AnimalsRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        IEnumerable<Animal> animals = GetAllAnimals();

        switch (orderBy.ToLower())
        {
            case "name":
                return animals.OrderBy(animal => animal.Name).ToList();
            case "description":
                return animals.OrderBy(animal => animal.Description).ToList();
            case "category":
                return animals.OrderBy(animal => animal.Category).ToList();
            case "area":
                return animals.OrderBy(animal => animal.Area).ToList();
        }
        
        return animals;
    }
    
    public IEnumerable<Animal> GetAllAnimals()
    {
        return GetAllAnimals("area");
    }

    public Animal GetAnimal(int idAnimal)
    {
        using var con = new SqlConnection(connectionString);
        con.Open(); 
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area, IndexNumber FROM Animal WHERE IdStudent = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var student = new Animal()
        {
            IdAnimal = (int)dr["IdAnimal"],
            Name = dr["Name"].ToString(),
            Description = dr["Description"].ToString(),
            Category = dr["Category"].ToString(),
            Area = dr["Area"].ToString(),
        };
        
        return student;
    }
    
    public int CreateAnimal(Animal animal)
    {
        using var con = new SqlConnection(connectionString);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    
    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(connectionString);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", id);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var con = new SqlConnection(connectionString);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}