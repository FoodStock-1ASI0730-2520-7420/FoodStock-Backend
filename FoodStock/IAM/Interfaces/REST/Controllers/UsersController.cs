using FoodStock.IAM.Domain.Services;
using FoodStock.IAM.Interfaces.REST.Assemblers;
using FoodStock.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.IAM.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserCommandService _commandService;

    public UsersController(IUserCommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPatch("select-plan")]
    public async Task<IActionResult> SelectPlan([FromBody] SelectPlanResource resource)
    {
        var command = SelectPlanCommandFromResourceAssembler.ToCommand(resource);
        var user = await _commandService.Handle(command);
        return Ok(new { ok = true, user });
    }
}