namespace Doggo.Infrastructure.Repositories;

using Domain.Entities.JobRequest;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class JobRequestRepository : AbstractRepository<JobRequest>, IJobRequestRepository
{
    private readonly DoggoDbContext _context;

    public JobRequestRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<JobRequest?> GetAsync(int dogId, CancellationToken cancellationToken = default)
    {
        return await _context.JobRequests.Where(x => x.Id == dogId)
            .Include(x => x.RequiredSchedule)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<JobRequest>> GetPageOfJobRequestsAsync(
        int count,
        int page,
        CancellationToken cancellationToken = default)
    {
        return await _context.JobRequests.OrderBy(ps => ps.Id)
            .Skip(count * (page - 1))
            .Take(count)
            .Include(x => x.RequiredSchedule)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}