namespace WarehouseApp.Model;

public class Warehouse
{
    public int IdWarehouse { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public Warehouse()
    {
    }

    public Warehouse(int idWarehouse, string name, string address)
    {
        IdWarehouse = idWarehouse;
        Name = name;
        Address = address;
    }
}