namespace WarehouseApp.Model;

public class Product
{
    public int IdProduct { get; set; }
    public string Name { get; set; }
    public int Description { get; set; }
    public double Price { get; set; }

    public Product(int idProduct, string name, int description, double price)
    {
        IdProduct = idProduct;
        Name = name;
        Description = description;
        Price = price;
    }
}