namespace Doggo.Application.Requests.Queries.DogOwner.GetPageOfDogOwnersQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.DogOwner;

public record GetPageOfDogOwnersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<DogOwnerResponse>>>;