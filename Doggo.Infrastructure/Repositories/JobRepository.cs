namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.Job;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class JobRepository : AbstractRepository<Job>, IJobRepository
{
    private readonly DoggoDbContext _context;

    public JobRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Job?> GetAsync(int dogId, CancellationToken cancellationToken = default)
    {
        return await _context.Jobs.Include(x => x.JobRequest).FirstOrDefaultAsync(x => x.Id == dogId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Job>> GetPageOfJobsAsync(int count, int page, CancellationToken cancellationToken = default)
    {
        return await _context.Jobs.OrderBy(ps => ps.Id)
                    .Skip(count * (page - 1))
                    .Take(count)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}