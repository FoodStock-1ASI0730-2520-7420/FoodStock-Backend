using FoodStock.Suppliers.Application.Internal.CommandServices;
using FoodStock.Suppliers.Application.Internal.QueryServices;
using FoodStock.Suppliers.Interfaces.REST.Resources;
using FoodStock.Suppliers.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Suppliers.Interfaces.REST;

[ApiController]
[Route("api/v1/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly CreateSupplierCommandService _createService;
    private readonly UpdateSupplierCommandService _updateService;
    private readonly DeleteSupplierCommandService _deleteService;
    private readonly GetAllSuppliersQueryService _getAllService;
    private readonly GetSupplierByIdQueryService _getByIdService;

    public SupplierController(
        CreateSupplierCommandService createService,
        UpdateSupplierCommandService updateService,
        DeleteSupplierCommandService deleteService,
        GetAllSuppliersQueryService getAllService,
        GetSupplierByIdQueryService getByIdService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
        _getAllService = getAllService;
        _getByIdService = getByIdService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierResource>>> GetAll()
    {
        var entities = await _getAllService.Handle();
        var resources = entities.Select(SupplierResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupplierResource>> GetById(int id)
    {
        var entity = await _getByIdService.Handle(id);
        if (entity is null) return NotFound();
        return Ok(SupplierResourceFromEntityAssembler.ToResource(entity));
    }

    [HttpPost]
    public async Task<ActionResult<SupplierResource>> Create([FromBody] CreateSupplierResource resource)
    {
        var created = await _createService.Handle(
            CreateSupplierCommandFromResourceAssembler.ToCommand(resource));
        var output = SupplierResourceFromEntityAssembler.ToResource(created);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, output);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SupplierResource>> Update(int id, [FromBody] EditSupplierResource resource)
    {
        var updated = await _updateService.Handle(
            UpdateSupplierCommandFromResourceAssembler.ToCommand(id, resource));
        if (updated is null) return NotFound();
        return Ok(SupplierResourceFromEntityAssembler.ToResource(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _deleteService.Handle(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
