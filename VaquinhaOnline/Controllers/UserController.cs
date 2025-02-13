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
    public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDto User, IFormFile ProfilePhoto,CancellationToken cancellationToken)
    {
        var result = await userService.UpdateUser(User, ProfilePhoto,cancellationToken);

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
    public async Task<IActionResult> GetUserById([FromRoute] Guid Id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var result = await userService.GetUserById(Id, cancellationToken);

        if (result.IsSucess)
            return Ok(result);

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet("profile/{fileName}")]
    public IActionResult GetProfilePhoto(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile", fileName);

        if (System.IO.File.Exists(filePath))
        {
            return File(System.IO.File.ReadAllBytes(filePath), "image/jpeg");
        }
        else
        {
            return NotFound();
        }
    }


    [HttpGet]
    [SwaggerOperation(
       Summary = "Recupera todos os utilizadores",
       Description = "Endpoint para recuperação de todos os utilizadores registados no sistema."
   )]
    public async Task<IActionResult> GetAllUsers([FromQuery] int PageNumber = 1, int PageSize = 12)
    {
        var cancellationToken = HttpContext.RequestAborted;

        var result = await userService.GetAllUsers(PageNumber, PageSize, cancellationToken);

        return result.IsSucess
            ? Ok(result)
            : NotFound("There is no users registred.");
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
       Summary = "Remove um utilizador",
       Description = "Endpoint para a remoção de um utilizador registado no sistema pelo id."
   )]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid Id, CancellationToken cancellationToken)
    {
        var result = await userService.DeleteUser(Id, cancellationToken);

        return result.Succeeded
            ? NoContent()
            : NotFound("The user was not found.");
    }
}
