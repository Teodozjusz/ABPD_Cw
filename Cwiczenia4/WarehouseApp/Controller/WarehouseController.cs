using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Model;
using WarehouseApp.Service;

namespace WarehouseApp.Controller;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _service = WarehouseService.GetInstance();

    [HttpPost]
    public IActionResult changeWarehouse(WarehousePayload payload)
    {
        return Ok(_service.ChangeWarehouse(payload));
    }
}