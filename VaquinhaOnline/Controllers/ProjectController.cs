using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaquinhaOnline.Application.Dtos.Investment;
using VaquinhaOnline.Application.Dtos.Project;
using VaquinhaOnline.Application.Features.Projects;

namespace VaquinhaOnline.Api.Controllers;

[Route("api/v1/project")]
[ApiController]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService projectService = projectService;

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cria um novo projecto",
        Description = "Endpoint para adicionar um novo projecto ao sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> CreateProject([FromForm] ProjectCreateDto project, IFormFile Image,CancellationToken cancellationToken)
    {
        var result = await projectService.CreateProject(project, Image, cancellationToken);

        return result.IsSucess
            ? Ok(new { Id = result.Value })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Remove um projecto",
        Description = "Endpoint para remover um projecto do sistema pelo Id."
    )]
    public async Task<IActionResult> DeleteProject([FromRoute] ProjectGetDto project, CancellationToken cancellationToken)
    {
        var result = await projectService.DeleteProject(project.Id, cancellationToken);

        if (result.IsSucess)
            return NoContent();

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Actualiza um projecto",
        Description = "Endpoint para remover um projecto do sistema pelo Id."
    )]
    public async Task<IActionResult> UpdateProject([FromRoute] ProjectUpdateDto project, CancellationToken cancellationToken)
    {
        var result = await projectService.UpdateProject(project, cancellationToken);

        if (result.IsSucess)
            return NoContent();

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Recupera um projecto",
        Description = "Endpoint para recuperação de um projecto do sistema pelo Id."
    )]
    public async Task<IActionResult> GetProjectById([FromRoute] Guid Id, CancellationToken cancellationToken)
    {
        var result = await projectService.GetProjectById(Id, cancellationToken);

        if (result.IsSucess)
            return Ok(result);

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Recupera todos os projectos",
        Description = "Endpoint para recuperação de todos os projectos registados no sistema."
    )]
    public async Task<IActionResult> GetAllProjects([FromQuery] int PageNumber = 1, int PageSize = 12)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var result = await projectService.GetAllProjects(PageNumber, PageSize, cancellationToken);

        return result.IsSucess
            ? Ok(result)
            : NotFound("There is no projects registred.");
    }

    [HttpGet("user")]
    [SwaggerOperation(
        Summary = "Recupera todos os projectos",
        Description = "Endpoint para recuperação de todos os projectos registados no sistema."
    )]
    public async Task<IActionResult> GetAllProjectsByUserId([FromQuery] int PageNumber = 1, int PageSize = 12)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var result = await projectService.GetAllProjectsByUserId(PageNumber, PageSize, cancellationToken);

        return result.IsSucess  
            ? Ok(result)
            : NotFound("There is no projects registred.");
    }

    [HttpGet("projects/{fileName}")]
    public IActionResult GetProjectImage(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "projects", fileName);

        if (System.IO.File.Exists(filePath))
        {
            return File(System.IO.File.ReadAllBytes(filePath), "image/jpeg");
        }
        else
        {
            return NotFound();
        }
    }
}

