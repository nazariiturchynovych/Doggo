namespace Doggo.Infrastructure.Repositories;

using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Base;
using Domain.Constants;
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

    public async Task<JobRequest?> GetAsync(Guid dogId, CancellationToken cancellationToken = default)
    {
        return await _context.JobRequests.Where(x => x.Id == dogId)
            .Include(x => x.RequiredSchedule)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<JobRequest>> GetDogOwnerJobRequests(
        Guid dogOwnerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.JobRequests.Where(x => x.DogOwnerId == dogOwnerId)
            .Include(x => x.RequiredSchedule)
            .OrderBy(ps => ps.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<JobRequest?> GetJobRequestWithJobsAsync(Guid jobRequestId)
    {
        return await _context.JobRequests
            .Include(x => x.Jobs)
            .FirstOrDefaultAsync(x => x.Id == jobRequestId);
    }

    public async Task<IReadOnlyCollection<JobRequest>> GetPageOfJobRequestsAsync(
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken = default)
    {
        IQueryable<JobRequest> jobRequestQuery = _context.JobRequests
            .Include(x => x.RequiredSchedule);
        if (!string.IsNullOrWhiteSpace(descriptionSearchTerm))
        {
            jobRequestQuery = jobRequestQuery.Where(
                x =>
                    x.Description.Contains(descriptionSearchTerm));
        }

        Expression<Func<JobRequest, object>> keySelector = sortColumn?.ToLower() switch
        {
            SortingConstants.Description => jobRequest => jobRequest.Description,
            SortingConstants.Salary => jobRequest => jobRequest.PaymentTo,
            _ => jobRequest => jobRequest.Id,
        };

        jobRequestQuery = sortOrder?.ToLower() == SortingConstants.Descending
            ? jobRequestQuery.OrderByDescending(keySelector)
            : jobRequestQuery.OrderBy(keySelector);

        return await jobRequestQuery
            .Skip(pageCount * (page - 1))
            .Take(pageCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}