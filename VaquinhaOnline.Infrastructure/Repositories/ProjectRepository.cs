using VaquinhaOnline.Domain.Interfaces.IRepository;

namespace VaquinhaOnline.Infrastructure.Repositories;

public class ProjectRepository (ApplicationDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public IQueryable<Project> GetAllProjects()
    {
        return context.Projects.AsQueryable();
    }

    public async Task<Project> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
       return await context.Projects.FirstOrDefaultAsync(p => p.Id == id);
    }
}
