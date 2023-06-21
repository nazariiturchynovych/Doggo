namespace Doggo.Infrastructure.Repositories;

using Abstractions;
using Domain.Entities.Walker.Schedule;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class PossibleScheduleRepository : AbstractRepository<PossibleSchedule>, IPossibleScheduleRepository
{
    private readonly DoggoDbContext _context;

    public PossibleScheduleRepository(DoggoDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<PossibleSchedule?> GetAsync(Guid possibleScheduleId, CancellationToken cancellationToken = default)
    {
        return await _context.PossibleSchedules.FirstOrDefaultAsync(x => x.Id == possibleScheduleId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<PossibleSchedule>> GetWalkerPossibleSchedulesAsync(
        Guid walkerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.PossibleSchedules.Where(x => x.WalkerId == walkerId)
            .OrderBy(ps => ps.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<PossibleSchedule>> GetPageOfPossibleSchedulesAsync(int pageCount, int page, CancellationToken cancellationToken = default)
    {
        return await _context.PossibleSchedules.OrderBy(ps => ps.Id)
                    .Skip(pageCount * (page - 1))
                    .Take(pageCount)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}