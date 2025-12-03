using FoodStock.Inventory.Domain.Model.Queries.Dishes;
using FoodStock.Inventory.Domain.Services;
using FoodStock.Inventory.Interfaces.REST.Resources.Dishes;
using FoodStock.Inventory.Interfaces.REST.Transform.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Inventory.Interfaces.REST;

[ApiController]
[Route("api/v1/dishes")]
public class DishController : ControllerBase
{
    private readonly IDishCommandService _cmd;
    private readonly IDishQueryService _qry;

    public DishController(IDishCommandService cmd, IDishQueryService qry) { _cmd = cmd; _qry = qry; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishResource>>> GetAll()
        => Ok((await _qry.Handle(new GetAllDishesQuery())).Select(DishResourceFromEntityAssembler.ToResource));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<DishResource>> GetById(long id)
    {
        var dish = await _qry.Handle(new GetDishByIdQuery(id));
        if (dish is null) return NotFound();
        return Ok(DishResourceFromEntityAssembler.ToResource(dish));
    }

    [HttpPost]
    public async Task<ActionResult<DishResource>> Create([FromBody] CreateDishResource r)
    {
        var created = await _cmd.Handle(CreateDishCommandFromResourceAssembler.ToCommand(r));
        var res = DishResourceFromEntityAssembler.ToResource(created);
        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<DishResource>> Edit(long id, [FromBody] EditDishResource r)
    {
        var updated = await _cmd.Handle(EditDishCommandFromResourceAssembler.ToCommand(id, r));
        return Ok(DishResourceFromEntityAssembler.ToResource(updated));
    }
}