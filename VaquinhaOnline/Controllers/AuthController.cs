using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaquinhaOnline.Application.Dtos.User;
using VaquinhaOnline.Application.Features.Users;

namespace VaquinhaOnline.Api.Controllers;

public class AuthController (IUserService userService, CancellationToken cancellationToken) : ControllerBase
{
    private readonly IUserService userService = userService;
    private readonly CancellationToken cancellationToken = cancellationToken;

    [HttpPost("signup")]
    [SwaggerOperation(
        Summary = "Cria um novo utilizador",
        Description = "Endpoint para adicionar um novo utilizador ao sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto User)
    {
        var result = await userService.CreateUser(User, cancellationToken);

        return result.IsSucess
            ? Ok(new { Id = result.Value })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login do utilizador",
        Description = "Endpoint para a início de sessão de um utilizador registado no sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var result = await userService.Login(loginRequest);

        return result.IsSucess
            ? Ok(new { Token = result.Value })
            : Unauthorized(new { Errors = result.Errors });
    }
}
