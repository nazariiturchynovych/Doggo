namespace Doggo.Application.Requests.Queries.DogOwner.GetPageOfDogOwnersQuery;

using Domain.Results;
using DTO;
using DTO.DogOwner;
using MediatR;

public record GetPageOfDogOwnersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetDogOwnerDto>>>;