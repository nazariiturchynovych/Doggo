namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.JobRequest.Schedules;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class RequiredScheduleRepository : AbstractRepository<RequiredSchedule>, IRequiredScheduleRepository
{
    private readonly DoggoDbContext _context;

    public RequiredScheduleRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<RequiredSchedule?> GetAsync(Guid dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.RequiredSchedules.FirstOrDefaultAsync(x => x.Id == dogOwnerId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<RequiredSchedule>> GetPageOfRequiredSchedulesAsync(int pageCount, int page, CancellationToken cancellationToken = default)
    {
        return await _context.RequiredSchedules.OrderBy(ps => ps.Id)
                    .Skip(pageCount * (page - 1))
                    .Take(pageCount)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}