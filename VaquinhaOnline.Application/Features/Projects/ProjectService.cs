
using FluentValidation;
using Microsoft.AspNetCore.Http;
using VaquinhaOnline.Domain.Entities;
using VaquinhaOnline.Domain.Interfaces.IRepository;
using VaquinhaOnline.Infrastructure.Repositories;

namespace VaquinhaOnline.Application.Features.Projects;

public class ProjectService(IProjectRepository projectRepository, IValidator<ProjectCreateDto> validator) : IProjectService
{
    private readonly IProjectRepository projectRepository = projectRepository;
    private readonly IValidator<ProjectCreateDto> validator = validator;

    public async Task<Result<Guid>> CreateProject(ProjectCreateDto projectDto, IFormFile Image,CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(projectDto);

        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(
                Error.Invalid("Error.Validation", validationResult.ToString())
            );
        }

        var project = new Project(
            title: projectDto.Title,
            description: projectDto.Description,
            sector: projectDto.Sector,
            status: projectDto.Status,
            goalValue: projectDto.GoalValue,
            closingDate: projectDto.ClosingDate,
            userId: projectDto.UserId
        );

        var result = await projectRepository.Create(project, cancellationToken);

        return result;
    }

    public async Task<Result> DeleteProject(Guid Id, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetProjectById(Id, cancellationToken);

        if (project == null)
        {
            return Result.Failure(Error.NotFound("NotFound", "The project was not found."));
        }

        var result = await projectRepository.Delete(project, cancellationToken);
        return result;
    }

    public async Task<ResultPaginated<List<ProjectGetDto>>> GetAllProjects(int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        var projectQuery = projectRepository.GetAllProjects();

        //Counting 
        var totalCount = projectQuery.Count();

        if (totalCount == 0)
        {
            return Result.Failure<ProjectGetDto>(
                error: Error.NotFound("NotFound", "No project found."),
                totalCount: totalCount,
                currentPage: PageNumber,
                pageSize: PageSize);
        }

        // Calculating the pagination
        var pageNumber = PageNumber < 1 ? 1 : PageNumber;
        var pageSize = PageSize < 1 ? 10 : PageSize;

        var projects = await projectQuery
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var projectsDtos = projects.Adapt<List<ProjectGetDto>>();

        return ResultPaginated<List<ProjectGetDto>>.Success(
            values: projectsDtos,
            totalCount: totalCount,
            currentPage: pageNumber,
            pageSize: pageSize);
    }

    public async Task<Result<ProjectGetDto>> GetProjectById(Guid Id, CancellationToken cancellationToken)
    {
        var existingProject = await projectRepository.GetProjectById(Id, cancellationToken);

        if (existingProject == null)
        {
            return Result.Failure<ProjectGetDto>(Error.NotFound("NotFound", "The project was not found."));
        }

        var dto = existingProject.Adapt<ProjectGetDto>();

        return Result.Succeed(dto);
    }

    public async Task<Result> UpdateProject(ProjectUpdateDto project, CancellationToken cancellationToken)
    {
        var dto = project.Adapt<ProjectCreateDto>();

        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(
                Error.Invalid("Error.Validation", validationResult.ToString())
            );
        }

        var existingProject = await projectRepository.GetProjectById(project.Id, cancellationToken);

        if (existingProject == null)
        {
            return Result.Failure(Error.NotFound("Error.NotFound", "The project was not found."));
        }

        existingProject.Update(
            title: existingProject.Title,
            description: existingProject.Description,
            sector: existingProject.Sector,
            currentValue: existingProject.CurrentValue
        );

        var result = await projectRepository.Update(existingProject, cancellationToken);
        return result;
    }
}
