using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaquinhaOnline.Application.Dtos.Investment;
using VaquinhaOnline.Application.Dtos.Project;
using VaquinhaOnline.Application.Features.Investments;
using VaquinhaOnline.Application.Features.Projects;

namespace VaquinhaOnline.Api.Controllers;

[Route("api/v1/investment")]
[ApiController]
public class InvestmentController (IInvestmentService investmentService) : ControllerBase
{
    private readonly IInvestmentService investmentService = investmentService;

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cria um novo investmento",
        Description = "Endpoint para adicionar um novo investimento ao sistema com os detalhes necessários."
    )]
    public async Task<IActionResult> CreateInvestment([FromBody] InvestmentCreateDto investment, CancellationToken cancellationToken)
    {
        var result = await investmentService.CreateInvestment(investment, cancellationToken);

        return result.IsSucess
            ? Ok(new { Id = result.Value })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Remove um investimento",
        Description = "Endpoint para remover um investimento do sistema pelo Id."
    )]
    public async Task<IActionResult> DeleteInvestment([FromRoute] InvestmentGetDto investment, CancellationToken cancellationToken)
    {
        var result = await investmentService.DeleteInvestment(investment.Id, cancellationToken);

        if (result.IsSucess)
            return NoContent();

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Remove um investimento",
        Description = "Endpoint para actualizar um investimento do sistema pelo Id."
    )]
    public async Task<IActionResult> UpdateProject([FromRoute] InvestmentUpdateDto investment, CancellationToken cancellationToken)
    {
        var result = await investmentService.UpdateInvestment(investment, cancellationToken);

        if (result.IsSucess)
            return NoContent();

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Recupera uma investimento",
        Description = "Endpoint para recuperação de uma investimento do sistema pelo Id."
    )]
    public async Task<IActionResult> GetInvestmentById([FromRoute] InvestmentGetDto investment, CancellationToken cancellationToken)
    {
        var result = await investmentService.GetInvestmentById(investment.Id, cancellationToken);

        if (result.IsSucess)
            return Ok(result);

        var error = result.Errors.FirstOrDefault();
        return error?.Code == "Error.NotFound"
            ? NotFound(new { Errors = result.Errors })
            : BadRequest(new { Errors = result.Errors });
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Recupera todos os investimentos",
        Description = "Endpoint para recuperação de todos os investimentos registados no sistema."
    )]
    public async Task<IActionResult> GetAllInvestments([FromQuery] int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        if (PageNumber < 1 || PageSize < 1)
        {
            return BadRequest("PageNumber & PageSize must be greater than 0.");
        }

        var result = await investmentService.GetAllInvestments(PageNumber, PageSize, cancellationToken);

        return result.IsSucess
            ? Ok(result)
            : NotFound("There is no investiment registred.");
    }
}
    