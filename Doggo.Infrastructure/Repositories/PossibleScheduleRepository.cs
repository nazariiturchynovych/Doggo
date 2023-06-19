namespace Doggo.Infrastructure.Repositories;

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

    public async Task<PossibleSchedule?> GetAsync(int dogOwnerId, CancellationToken cancellationToken = default)
    {
        return await _context.PossibleSchedules.FirstOrDefaultAsync(x => x.Id == dogOwnerId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<PossibleSchedule>> GetPageOfPossibleSchedulesAsync(int count, int page, CancellationToken cancellationToken = default)
    {
        return await _context.PossibleSchedules.OrderBy(ps => ps.Id)
                    .Skip(count * (page - 1))
                    .Take(count)
                    .ToListAsync(cancellationToken: cancellationToken);
    }
}