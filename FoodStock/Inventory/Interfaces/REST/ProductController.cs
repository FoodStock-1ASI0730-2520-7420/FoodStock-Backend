using FoodStock.Inventory.Domain.Model.Queries.Products;
using FoodStock.Inventory.Domain.Services;
using FoodStock.Inventory.Interfaces.REST.Resources.Products;
using FoodStock.Inventory.Interfaces.REST.Transform.Products;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.Inventory.Interfaces.REST;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductCommandService _cmd;
    private readonly IProductQueryService _qry;
    public ProductController(IProductCommandService cmd, IProductQueryService qry) { _cmd = cmd; _qry = qry; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResource>>> GetAll()
        => Ok((await _qry.Handle(new GetAllProductsQuery())).Select(ProductResourceFromEntityAssembler.ToResource));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductResource>> GetById(long id)
    {
        var e = await _qry.Handle(new GetProductByIdQuery(id));
        if (e is null) return NotFound();
        return Ok(ProductResourceFromEntityAssembler.ToResource(e));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResource>> Create([FromBody] CreateProductResource r)
    {
        var created = await _cmd.Handle(CreateProductCommandFromResourceAssembler.ToCommand(r));
        var res = ProductResourceFromEntityAssembler.ToResource(created);
        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductResource>> Edit(long id, [FromBody] EditProductResource r)
    {
        var updated = await _cmd.Handle(EditProductCommandFromResourceAssembler.ToCommand(id, r));
        return Ok(ProductResourceFromEntityAssembler.ToResource(updated));
    }

    [HttpPut("{id:long}/consume")]
    public async Task<ActionResult<ProductResource>> Consume(long id, [FromBody] ConsumeProductResource r)
    {
        var updated = await _cmd.Handle(ConsumeProductCommandFromResourceAssembler.ToCommand(id, r));
        return Ok(ProductResourceFromEntityAssembler.ToResource(updated));
    }
}