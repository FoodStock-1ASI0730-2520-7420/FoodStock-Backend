using System.Net.Mime;
using FoodStock.Sales.Domain.Model.Queries;
using FoodStock.Sales.Domain.Services;
using FoodStock.Sales.Interfaces.REST.Resources;
using FoodStock.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodStock.Sales.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Sale endpoints")]
public class SaleController(
    ISaleQueryService saleQueryService,
    ISaleCommandService saleCommandService) : ControllerBase
{
    [HttpGet("{saleId:int}")]
    [SwaggerOperation(
        Summary = "Get a sale by its id",
        Description = "Get a sale by its id",
        OperationId = "GetSaleById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The sale was found", typeof(SaleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The sale was not found")]
    public async Task<IActionResult> GetSaleById([FromRoute] int saleId)
    {
        var sale = await saleQueryService.Handle(new GetSaleByIdQuery(saleId));
        if (sale is null) return NotFound();
        var saleResource = SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return Ok(saleResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a sale",
        Description = "Create a sale",
        OperationId = "CreateSale")]
    [SwaggerResponse(StatusCodes.Status201Created, "The sale was created", typeof(SaleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The sale was not created")]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleResource resource)
    {
        var createSaleCommand = CreateSaleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var sale = await saleCommandService.Handle(createSaleCommand);
        if (sale is null) return BadRequest();
        var saleResource = SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return CreatedAtAction(nameof(GetSaleById), new { saleId = sale.Id }, saleResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all sales",
        Description = "Get all sales",
        OperationId = "GetAllSales")]
    [SwaggerResponse(StatusCodes.Status200OK, "The sale were found", typeof(IEnumerable<SaleResource>))]
    public async Task<IActionResult> GetAllSales()
    {
        var getAllSalesQuery = new GetAllSalesQuery();
        var sales = await saleQueryService.Handle(getAllSalesQuery);
        var saleResources = sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(saleResources);
    }
    
    [HttpPut("{saleId:int}")]
    [SwaggerOperation(
        Summary = "Update a sale",
        Description = "Update a sale",
        OperationId = "UpdateSale")]
    [SwaggerResponse(StatusCodes.Status200OK, "The sale was updated", typeof(SaleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The sale was not found")]
    public async Task<IActionResult> UpdateSale([FromRoute] int saleId, [FromBody] UpdateSaleResource resource)
    {
        if(saleId != resource.Id)
            return BadRequest();
        var updateSaleCommand = UpdateSaleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var sale = await saleCommandService.Handle(updateSaleCommand);
        if (sale is null) return NotFound();
        var saleResource = SaleResourceFromEntityAssembler.ToResourceFromEntity(sale);
        return Ok(saleResource);
    }
    
    [HttpDelete("{saleId:int}")]
    [SwaggerOperation(
        Summary = "Delete a sale",
        Description = "Delete a sale",
        OperationId = "DeleteSale")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The sale was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The sale was not found")]
    public async Task<IActionResult> DeleteSale([FromRoute] int saleId)
    {
        var deleteSaleResource = new DeleteSaleResource(saleId);
        var deleteSaleCommand = DeleteSaleCommandFromResourceAssembler.ToCommandFromResource(deleteSaleResource);
        await saleCommandService.Handle(deleteSaleCommand);
        return NoContent();
    }
}