namespace WarehouseApp.Model;

public interface IWarehouseService
{
    public Task<int> ChangeWarehouse(WarehousePayload payload);
}