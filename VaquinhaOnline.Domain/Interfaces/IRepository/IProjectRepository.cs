using VaquinhaOnline.Domain.Entities;

namespace VaquinhaOnline.Domain.Interfaces.IRepository;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<Project> GetProjectById(Guid id, CancellationToken cancellationToken);
    IQueryable<Project> GetAllProjects();
    IQueryable<Project> GetAllProjectsByUserId(Guid Id);
}
