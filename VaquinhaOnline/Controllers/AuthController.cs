using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaquinhaOnline.Application.Dtos.User;
using VaquinhaOnline.Application.Features.Users;

namespace VaquinhaOnline.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("signup")]
    [SwaggerOperation(
        Summary = "Cria um novo utilizador",
        Description = "Endpoint para adicionar um novo utilizador ao sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> CreateUser([FromForm] UserCreateDto user, IFormFile profilePhoto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var result = await _userService.CreateUser(user, profilePhoto,cancellationToken);

        return result.IsSucess
            ? Ok(new { Id = result.Value })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login do utilizador",
        Description = "Endpoint para início de sessão de um utilizador registado no sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var result = await _userService.Login(loginRequest);

        return result.IsSucess
            ? Ok(new { Token = result.Value })
            : Unauthorized(new { Errors = result.Errors });
    }
}