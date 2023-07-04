namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Application.Abstractions.Persistence.Read;
using Base;
using Domain.Constants;
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

    public async Task<Job?> GetAsync(Guid dogId, CancellationToken cancellationToken = default)
    {
        return await _context.Jobs
            .Include(x => x.JobRequest.RequiredSchedule)
            .Include(x => x.Dog)
            .FirstOrDefaultAsync(x => x.Id == dogId, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Job>> GetDogOwnerJobsAsync(
        Guid dogOwnerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Jobs.Where(x => x.DogOwnerId == dogOwnerId)
            .Include(x => x.JobRequest.RequiredSchedule)
            .Include(x => x.Dog)
            .OrderBy(ps => ps.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Job>> GetDogJobsAsync(
        Guid dogId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Jobs.Where(x => x.DogId == dogId)
            .Include(x => x.JobRequest.RequiredSchedule)
            .Include(x => x.Dog)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Job>> GetWalkerJobsAsync(
        Guid jobId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Jobs.Where(x => x.WalkerId == jobId)
            .Include(x => x.JobRequest.RequiredSchedule)
            .Include(x => x.Dog)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }


public async Task<IReadOnlyCollection<Job>> GetPageOfJobsAsync(
        string? commentSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Job> jobQuery = _context.Jobs
            .Include(x => x.JobRequest.RequiredSchedule)
            .Include(x => x.Dog);
        if (!string.IsNullOrWhiteSpace(commentSearchTerm))
        {
            jobQuery = jobQuery.Where(
                x =>
                    x.Comment.Contains(commentSearchTerm));
        }

        Expression<Func<Job, object>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.Comment => job => job.Comment,
            SortingConstants.Salary => job => job.Payment,
            _ => job => job.Id,
        };

        jobQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? jobQuery.OrderByDescending(keySelector)
            : jobQuery.OrderBy(keySelector);

        return await jobQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}