using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaquinhaOnline.Application.Dtos.User;
using VaquinhaOnline.Application.Features.Users;

namespace VaquinhaOnline.Api.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController (IUserService userService) : Controller
{
    private readonly IUserService userService = userService;

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Actualiza dados do utilizador",
        Description = "Endpoint para actualizar os dados de registo de um utilizador do sistema pelo Id."
    )]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto User, CancellationToken cancellationToken)
    {
        var result = await userService.UpdateUser(User, cancellationToken);

        if (result.IsSucess)
            return NoContent();

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Recupera um utilizador",
        Description = "Endpoint para recuperação de um utilizador do sistema pelo Id."
    )]
    public async Task<IActionResult> GetUserById([FromRoute] UserGetDto User)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var result = await userService.GetUserById(User.Id, cancellationToken);

        if (result.IsSucess)
            return Ok(result);

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet]
    [SwaggerOperation(
       Summary = "Recupera todos os utilizadores",
       Description = "Endpoint para recuperação de todos os utilizadores registados no sistema."
   )]
    public async Task<IActionResult> GetAllUsers([FromQuery] int PageNumber, int PageSize)
    {
        var cancellationToken = HttpContext.RequestAborted;
        if (PageNumber < 1 || PageSize < 1)
        {
            return BadRequest("PageNumber & PageSize must be greater than 0.");
        }

        var result = await userService.GetAllUsers(PageNumber, PageSize, cancellationToken);

        return result.IsSucess
            ? Ok(result)
            : NotFound("There is no users registred.");
    }
}
