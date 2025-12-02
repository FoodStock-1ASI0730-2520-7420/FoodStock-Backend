// path: src/Reservations/Interfaces/REST/Controllers/TablesController.cs
using System.Net.Mime;
using FoodStock.Reservations.Application.Internal.CommandServices;
using FoodStock.Reservations.Application.Internal.QueryServices;
using FoodStock.Reservations.Interfaces.REST.Resources;
using FoodStock.Reservations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodStock.Reservations.Interfaces.REST.Controllers;

[ApiController]
[Route("tables")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Tables")]
public class TablesController(TableQueryService query, TableCommandService commands) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List tables", OperationId = "ListTables")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tables found", typeof(IEnumerable<TableResource>))]
    public async Task<IActionResult> ListAsync()
        => Ok((await query.ListAsync()).Select(TableResourceAssembler.ToResource));

    [HttpPost]
    [SwaggerOperation(Summary = "Create table", OperationId = "CreateTable")]
    [SwaggerResponse(StatusCodes.Status201Created, "Table created", typeof(TableResource))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTableResource body)
    {
        var created = await commands.CreateAsync(body.Number, body.Capacity);
        var res = TableResourceAssembler.ToResource(created);
        return CreatedAtAction(nameof(ListAsync), new { id = res.Id }, res);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete table", OperationId = "DeleteTable")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Table deleted")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await commands.DeleteAsync(id);
        return NoContent();
    }
}