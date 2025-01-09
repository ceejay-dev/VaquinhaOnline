namespace VaquinhaOnline.Application.Features.Projects;

public interface IProjectService
{
    Task<Result<Guid>> CreateProject(ProjectCreateDto project, CancellationToken cancellationToken);
    Task<Result> DeleteProject(Guid Id, CancellationToken cancellationToken);
    Task<Result> UpdateProject(ProjectUpdateDto project, CancellationToken cancellationToken);
    Task<Result<ProjectGetDto>> GetProjectById(Guid Id, CancellationToken cancellationToken);
    Task<ResultPaginated<List<ProjectGetDto>>> GetAllProjects(int PageNumber, int PageSize, CancellationToken cancellationToken);
}