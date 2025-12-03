using FoodStock.IAM.Domain.Services;
using FoodStock.IAM.Interfaces.REST.Assemblers;
using FoodStock.IAM.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

namespace FoodStock.IAM.Interfaces.REST.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public AuthenticationController(IUserCommandService commandService, IUserQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommand(resource);
        var user = await _commandService.Handle(command);
        return Ok(new { ok = true, user });
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var command = SignInCommandFromResourceAssembler.ToCommand(resource);
        var result = await _queryService.Handle(command);
        if (result is null) return Unauthorized(new { ok = false, message = "Credenciales inválidas" });
        return Ok(new { ok = true, user = result.Value.User, token = result.Value.Token });
    }
}