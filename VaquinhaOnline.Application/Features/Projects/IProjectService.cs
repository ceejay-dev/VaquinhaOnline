namespace VaquinhaOnline.Application.Features.Projects;

public interface IProjectService
{
    Task<Result<Guid>> CreateProject(ProjectCreateDto projectDto, CancellationToken cancellationToken); 
}